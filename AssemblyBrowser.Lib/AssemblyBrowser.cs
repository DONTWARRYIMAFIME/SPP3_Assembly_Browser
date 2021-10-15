using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblyBrowser.Lib.Extensions;
using AssemblyBrowser.Lib.TreeComponent;
using static System.Reflection.BindingFlags;

//TODO: implement optional task (extension methods)
namespace AssemblyBrowser.Lib
{
    public static class AssemblyBrowser
    {
        private static readonly List<ExtensionMethodNode> Extensions = new();

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
                        assemblyInfo.Add(type.Namespace, new NamespaceNode(type.Namespace));
                    }

                    var namespaceNode = assemblyInfo[type.Namespace];
                    var typeNode = CreateTypeNode(type);
                    
                    namespaceNode.AddNode(typeNode);
                    
                    typeNode.AddRange(GetFieldNodes(type));
                    typeNode.AddRange(GetPropertyNodes(type));
                    typeNode.AddRange(GetConstructorNodes(type));
                    typeNode.AddRange(GetMethodNodes(type));
                    
                    Extensions.AddRange(GetExtensionMethodNodes(type));

                    //Extensions.Add(typeNode.Type, GetExtensionMethodNodes(type));
                    //typeNode.AddRange();

                    // if (type.IsDefined(typeof(ExtensionAttribute), false))
                    //     assemblyInfo = GetExtensionNamespaces(type, assemblyInfo);
                }
                else
                {
                    Console.WriteLine("Namespace of type: " + type + " is null");
                }
            }
            
            var result = assemblyInfo.Values.ToList();
            InsertExtensionMethods(result);

            return result;
        }

        private static TypeNode CreateTypeNode(Type type)
        {
            var accessModifier = type.GetAccessModifier();
            var typeModifier = type.GetTypeModifier();
            var classType = type.GetClassType();
            var name = type.Name;
            
            return new TypeNode(accessModifier, typeModifier, classType, name);
        }

        private static IEnumerable<FieldNode> GetFieldNodes(Type type)
        {
            return (from fieldInfo in type.GetFields()
                let accessModifier = fieldInfo.GetAccessModifier()
                let typeModifier = fieldInfo.GetTypeModifier()
                let filedType = fieldInfo.FieldType.ToGenericTypeString()
                let name = fieldInfo.Name
                select new FieldNode(accessModifier, typeModifier, filedType, name))
                .ToList();
        }
        
        private static IEnumerable<PropertyNode> GetPropertyNodes(Type type)
        {
            return (from propertyInfo in type.GetProperties()
                    let accessModifier = propertyInfo.GetGetMethod(true).GetAccessModifier()
                    let propertyType = propertyInfo.PropertyType.ToGenericTypeString()
                    let name = propertyInfo.Name
                    let accessors = GetAccessors(propertyInfo)
                    select new PropertyNode(accessModifier, propertyType, name, accessors))
                .ToList();
        }

        private static IEnumerable<AccessorNode> GetAccessors(PropertyInfo propertyInfo)
        {
            return (from accessor in propertyInfo.GetAccessors(true)
                    let accessModifier = accessor.GetAccessModifier()
                    let name = accessor.Name
                    select new AccessorNode(accessModifier, name))
                .ToList();
        }

        private static IEnumerable<ConstructorNode> GetConstructorNodes(Type type)
        {
            return (from constructor in type.GetConstructors()
                    let accessModifier = constructor.GetAccessModifier()
                    let name = type.Name
                    let parameters = GetParameterNodes(constructor.GetParameters())
                    select new ConstructorNode(accessModifier, name, parameters))
                .ToList();
        }
        
        private static IEnumerable<MethodNode> GetMethodNodes(Type type)
        {
            return (from method in type.GetMethods(Instance | Static | Public | NonPublic | DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Where(m => !m.IsDefined(typeof(ExtensionAttribute), false))
                    let accessModifier = method.GetAccessModifier()
                    let typeModifier = method.GetTypeModifier()
                    let returnType = method.ReturnType.ToGenericTypeString()
                    let name = method.Name
                    let parameters = GetParameterNodes(method.GetParameters())
                    select new MethodNode(accessModifier, typeModifier, returnType, name, parameters))
                .ToList();
        }

        private static IEnumerable<ParameterNode> GetParameterNodes(IEnumerable<ParameterInfo> parameters)
        {
            return (from parameter in parameters
                    let typeModifier = parameter.GetTypeModifier()
                    let parameterType = parameter.ParameterType.ToGenericTypeString()
                    let name = parameter.Name
                    select new ParameterNode(typeModifier, parameterType, name))
                .ToList();
        }
        
        private static IEnumerable<ExtensionMethodNode> GetExtensionMethodNodes(Type type)
        {
            return (from method in type.GetMethods(Instance | Static | Public | NonPublic | DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Where(m => m.IsDefined(typeof(ExtensionAttribute), false))
                    let accessModifier = method.GetAccessModifier()
                    let typeModifier = method.GetTypeModifier()
                    let returnType = method.ReturnType.ToGenericTypeString()
                    let name = method.Name
                    let parameters = GetParameterNodes(method.GetParameters())
                    select new ExtensionMethodNode(accessModifier, typeModifier, returnType, name, parameters))
                .ToList();
        }

        private static void InsertExtensionMethods(List<INode> nodes)
        {
            foreach (var extensionMethod in Extensions)
            {
                var extendedType = extensionMethod.Nodes[0].Type;
                foreach (var namespaceNode in nodes)
                {
                    foreach (var typeNode in namespaceNode.Nodes)
                    {
                        if (typeNode.Type == extendedType)
                        {
                            typeNode.AddNode(extensionMethod);
                        }
                    }
                }
            }
        }

    }
}