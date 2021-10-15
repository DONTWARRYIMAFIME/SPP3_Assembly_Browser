using System.Reflection;

namespace AssemblyBrowser.Lib.Extensions
{
    public static class MethodBaseExtensions
    {
        public static string GetAccessModifier(this MethodBase constrInfo)
        {
            if (constrInfo.IsPublic)
                return "public";
            if (constrInfo.IsPrivate)
                return "private";
            if (constrInfo.IsFamily)
                return "protected";
            if (constrInfo.IsAssembly)
                return "internal";
            if (constrInfo.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }
        
        public static string GetTypeModifier(this MethodBase methodBase)
        {
            return methodBase.IsStatic ? "static" : "";
        }
    }
}