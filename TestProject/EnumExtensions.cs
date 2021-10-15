using System;
using static TestProject.EnumWithExtensions;

namespace TestProject
{
    public static class EnumExtensions
    {
        public static string GetString(this EnumWithExtensions enumValue)
        {
            return enumValue switch
            {
                FirstEnumValue => "First",
                SecondEnumValue => "Second",
                _ => ""
            };
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