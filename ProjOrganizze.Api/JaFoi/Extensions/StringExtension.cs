using System.Globalization;

namespace ProjOrganizze.Api.Extensions
{
    public static class StringExtension
    {
        public static DateTime ToDateTime(this string value)
        {
                DateTime date = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return date;
        }
    }
}
