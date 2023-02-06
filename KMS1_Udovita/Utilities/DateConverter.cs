using System;

namespace KMS1_Udovita
{
    public class DateConverter
    {
        public static DateTime ConvertDate(string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
