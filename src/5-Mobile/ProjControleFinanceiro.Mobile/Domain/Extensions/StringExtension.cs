using System.Globalization;

namespace ProjControleFinanceiro.Mobile.Domain.Extensions
{
    public static class StringExtension
    {
        public static DateTime ToDateTime(this string value)
        {
            DateTime date = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return date;
        }

        public static string ToCapitalize(this string value)
        {
            string capitalize = string.Empty;
            if (value.Length == 0)
                capitalize = string.Empty;
            else if (value.Length == 1)
                capitalize = (char.ToUpper(value[0])).ToString();
            else
                capitalize = char.ToUpper(value[0]) + value.Substring(1);
            return capitalize;
        }
    }
}
