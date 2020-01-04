using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ExtentionMethods {

    public static class ExtensionMethods {
        public static string GetDescription (this Enum GenericEnum) {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0) {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (_Attribs != null && _Attribs.Count() > 0) {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        public static string SplitCamelCase (this string str) {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

    }

}
