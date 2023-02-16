using System;
using System.Linq;

namespace Ufynd.Task1.WebApi.Common
{
    public static class StringUtilityExtensions
    {
        public static string ToJustNumbers(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return new String(str.Where(Char.IsDigit).ToArray());
        }
    }
}
