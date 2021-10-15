using System.Reflection;

namespace AssemblyBrowser.Lib.Extensions
{
    public static class MethodInfoExtensions
    {
        public static string GetAccessModifier(this MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
                return "public";
            if (methodInfo.IsPrivate)
                return "private";
            if (methodInfo.IsFamily)
                return "protected";
            if (methodInfo.IsAssembly)
                return "internal";
            if (methodInfo.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }
        
        public static string GetTypeModifier(this MethodInfo methodInfo)
        {
            if (methodInfo.IsAbstract)
                return "abstract";
            if (methodInfo.IsStatic)
                return "static";
            if (methodInfo.IsVirtual)
                return "virtual";
            if (methodInfo.GetBaseDefinition() != methodInfo)
                return "override";

            return "";
        }
    }
}