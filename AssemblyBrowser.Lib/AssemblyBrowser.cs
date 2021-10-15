using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblyBrowser.Lib.Extensions;
using AssemblyBrowser.Lib.TreeComponent;
using static System.Reflection.BindingFlags;

//TODO: implement unit tests
namespace AssemblyBrowser.Lib
{
    public static class AssemblyBrowser
    {
        private static readonly List<Node> Extensions = new();

        public static List<INode> GetAssemblyInfo(string filePath)
        {
            var assembly = Assembly.LoadFrom(filePath);
            var assemblyInfo = new Dictionary<string, INode>();
            
            foreach (var type in assembly.GetTypes())
            {
                if (type.Namespace != null)
                {
                    if (!assemblyInfo.ContainsKey(type.Namespace))
                    {
                        assemblyInfo.Add(type.Namespace, new Node("[namespace]", name:type.Namespace));
                    }

                    var namespaceNode = assemblyInfo[type.Namespace];
                    var typeNode = CreateTypeNode(type);
                    
                    namespaceNode.AddNode(typeNode);
                    
                    typeNode.AddRange(GetFieldNodes(type));
                    typeNode.AddRange(GetPropertyNodes(type));
                    typeNode.AddRange(GetConstructorNodes(type));
                    typeNode.AddRange(GetMethodNodes(type));
                    
                    Extensions.AddRange(GetExtensionMethodNodes(type));
                }
                else
                {
                    Console.WriteLine("Namespace of type: " + type + " is null");
                }
            }
            
            var result = assemblyInfo.Values.ToList();
            InsertExtensionMethods(result);
            Extensions.Clear();
            return result;
        }

        private static Node CreateTypeNode(Type type)
        {
            var accessModifier = type.GetAccessModifier();
            var typeModifier = type.GetTypeModifier();
            var classType = type.GetClassType();
            var fullType = type.FullName;
            var name = type.Name;
            
            return new Node("[type]", accessModifier:accessModifier, typeModifier:typeModifier, classType:classType, type:name, fullType:fullType);
        }

        private static IEnumerable<Node> GetFieldNodes(Type type)
        {
            return (from fieldInfo in type.GetFields()
                let accessModifier = fieldInfo.GetAccessModifier()
                let typeModifier = fieldInfo.GetTypeModifier()
                let fieldType = fieldInfo.FieldType.ToGenericTypeString()
                let fullType = fieldInfo.FieldType.FullName
                let name = fieldInfo.Name
                select new Node("[field]", accessModifier:accessModifier, typeModifier:typeModifier, type:fieldType, fullType:fullType, name:name))
                .ToList();
        }
        
        private static IEnumerable<Node> GetPropertyNodes(Type type)
        {
            return (from propertyInfo in type.GetProperties()
                    let accessModifier = propertyInfo.GetGetMethod(true).GetAccessModifier()
                    let propertyType = propertyInfo.PropertyType.ToGenericTypeString()
                    let fullType = propertyInfo.PropertyType.FullName
                    let name = propertyInfo.Name
                    let accessors = GetAccessors(propertyInfo)
                    select new Node("[property]", accessModifier:accessModifier, type:propertyType, fullType:fullType, name:name, nodes:accessors))
                .ToList();
        }

        private static IEnumerable<Node> GetAccessors(PropertyInfo propertyInfo)
        {
            return (from accessor in propertyInfo.GetAccessors(true)
                    let accessModifier = accessor.GetAccessModifier()
                    let name = accessor.Name
                    select new Node("[accessor]", accessModifier:accessModifier, name:name))
                .ToList();
        }

        private static IEnumerable<Node> GetConstructorNodes(Type type)
        {
            return (from constructor in type.GetConstructors()
                    let accessModifier = constructor.GetAccessModifier()
                    let name = type.Name
                    let parameters = GetParameterNodes(constructor.GetParameters())
                    select new Node("[constructor]", accessModifier:accessModifier, name:name, nodes:parameters))
                .ToList();
        }
        
        private static IEnumerable<Node> GetMethodNodes(Type type)
        {
            return (from method in type.GetMethods(Instance | Static | Public | NonPublic | DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    let accessModifier = method.GetAccessModifier()
                    let typeModifier = method.GetTypeModifier()
                    let returnType = method.ReturnType.ToGenericTypeString()
                    let name = method.Name
                    let parameters = GetParameterNodes(method.GetParameters())
                    select new Node("[method]", accessModifier:accessModifier, typeModifier:typeModifier, returnType:returnType, name:name, nodes:parameters))
                .ToList();
        }

        private static IEnumerable<Node> GetParameterNodes(IEnumerable<ParameterInfo> parameters)
        {
            return (from parameter in parameters
                    let typeModifier = parameter.GetTypeModifier()
                    let parameterType = parameter.ParameterType.ToGenericTypeString()
                    let fullType = parameter.ParameterType.FullName
                    let name = parameter.Name
                    select new Node("[param]", typeModifier:typeModifier, type:parameterType, fullType:fullType, name:name))
                .ToList();
        }
        
        private static IEnumerable<Node> GetExtensionMethodNodes(Type type)
        {
            return (from method in type.GetMethods(Instance | Static | Public | NonPublic | DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Where(m => m.IsDefined(typeof(ExtensionAttribute), false))
                    let accessModifier = method.GetAccessModifier()
                    let typeModifier = method.GetTypeModifier()
                    let fullType = method.ReturnType.FullName
                    let returnType = method.ReturnType.ToGenericTypeString()
                    let name = method.Name
                    let parameters = GetParameterNodes(method.GetParameters())
                    select new Node("[method]", optional:"[extension]", accessModifier:accessModifier, typeModifier:typeModifier, fullType:fullType, returnType:returnType, name:name, nodes:parameters))
                .ToList();
        }

        private static void InsertExtensionMethods(List<INode> nodes)
        {
            foreach (var extensionMethod in Extensions)
            {
                var extendedType = extensionMethod.Nodes[0].FullType;
                foreach (var namespaceNode in nodes)
                {
                    foreach (var typeNode in namespaceNode.Nodes)
                    {
                        if (typeNode.FullType == extendedType)
                        {
                            typeNode.AddNode(extensionMethod);
                        }
                    }
                }
            }
        }

    }
}