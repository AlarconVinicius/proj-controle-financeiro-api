using System.Globalization;

namespace ProjControleFinanceiro.Mobile.Domain.Converters
{
    public class TransacaoNomeCorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.White;
            var random = new Random();
            var color = String.Format("#FF{0:X6}", random.Next(0x1000000));
            return Color.FromArgb(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}