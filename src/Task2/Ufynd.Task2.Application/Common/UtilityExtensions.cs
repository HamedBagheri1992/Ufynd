using System;
using System.Globalization;

namespace Ufynd.Task2.Application.Common
{
    public static class UtilityExtensions
    {
        static CultureInfo franceCulture = CultureInfo.CreateSpecificCulture("fr-FR");

        public static string ToMyDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yy");
        }

        public static string ToMyPriceFormat(this double price)
        {
            return price.ToString("0.00", franceCulture);
        }
    }
}
