using System;
using System.Reflection;
using WcfExample.Shared.Attributes;

namespace WcfExample.Shared.Extensions
{
    public static class EnumExtension
    {
        public static string GetStringValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<StringValueAttribute>();
            return attr?.Value;
        }
    }
}
