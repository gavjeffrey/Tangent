using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tangent.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First().Name;
        }

        public static string GetEnumValueFromEnumMemberAttribute(this Enum enumValue)
        {
            return ((EnumMemberAttribute)
                        enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttributes(typeof(EnumMemberAttribute), true)
                            .GetValue(0)).Value;
        }
    }
}
