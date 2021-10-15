using System.Reflection;

namespace AssemblyBrowser.Lib.Extensions
{
    public static class FieldInfoExtensions
    {
        public static string GetAccessModifier(this FieldInfo filedInfo)
        {
            if (filedInfo.IsPublic)
                return "public";
            if (filedInfo.IsPrivate)
                return "private";
            if (filedInfo.IsFamily)
                return "protected";
            if (filedInfo.IsAssembly)
                return "internal";
            if (filedInfo.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }
        
        public static string GetTypeModifier(this FieldInfo fieldInfo)
        {
            return fieldInfo.IsStatic ? "static" : "";
        }
    }
}