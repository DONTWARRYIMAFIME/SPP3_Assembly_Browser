using System;
using System.Linq;

namespace AssemblyBrowser.Lib.Extensions
{
    public static class TypeExtensions
    {
        public static string ToGenericTypeString(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;
            
            var genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')];
            var genericArgs = string.Join(", ",
                type.GetGenericArguments()
                    .Select(ToGenericTypeString).ToArray());
            return genericTypeName + "<" + genericArgs + ">";
        }
        
        public static string ToGenericTypeString(this Type[] types)
        {
            var listTypes = types.Select(type => type.ToGenericTypeString()).ToList();
            return "<" + string.Join(", ", listTypes) + ">";
        }
        
        public static string GetAccessModifier(this Type type)
        {
            if (type.IsNestedPublic || type.IsPublic)
                return "public";
            if (type.IsNestedPrivate)
                return "private";
            if (type.IsNestedFamily)
                return "protected";
            if (type.IsNestedAssembly)
                return "internal";
            if (type.IsNestedFamORAssem)
                return "protected internal";
            if (type.IsNestedFamANDAssem)
                return "private protected";
            if (type.IsNotPublic)
                return "private";

            return "";
        }
        
        public static string GetClassType(this Type type)
        {
            if (type.GetMethods().Any(m => m.Name == "<Clone>$"))
                return "record";
            if (type.IsClass)
                return "class";
            if (type.IsEnum)
                return "enum";
            if (type.IsInterface)
                return "interface";
            if (type.IsGenericType)
                return "generic";
            if (type.IsValueType && !type.IsPrimitive)
                return "structure";

            return "";
        }
        
        public static string GetTypeModifier(this Type type)
        {
            if (type.IsAbstract && type.IsSealed)
                return "static";
            if (type.IsAbstract)
                return "abstract";
            if (type.IsSealed)
                return "sealed";

            return "";
        }
    }
}