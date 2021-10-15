using System;
using static TestProject.EnumWithExtensions;

namespace TestProject
{
    public enum EnumWithExtensions
    {
        Unknown,
        FirstEnumValue,
        SecondEnumValue
    }

    static class EnumExtensions
    {
        public static string GetString(this EnumWithExtensions enumValue)
        {
            switch (enumValue)
            {
                case FirstEnumValue: return "First";
                case SecondEnumValue: return "Second";
                default: return "";
            }
        }
        
        public static EnumWithExtensions ValueOf(this EnumWithExtensions enumValue, string value)
        {
            if (value.Equals("FirstEnumValue", StringComparison.OrdinalIgnoreCase))
            {
                return FirstEnumValue;
            }
            
            if (value.Equals("SecondEnumValue", StringComparison.OrdinalIgnoreCase))
            {
                return SecondEnumValue;
            }

            return Unknown;
        }
    }
}