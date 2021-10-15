using System.Reflection;

namespace AssemblyBrowser.Lib.Extensions
{
    public static class ParameterInfoExtensions
    {
        public static string GetTypeModifier(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.IsRetval)
                return "ret";
            if (parameterInfo.IsIn)
                return "in";
            if (parameterInfo.IsOut)
                return "virtual";

            return "";
        }
    }
}