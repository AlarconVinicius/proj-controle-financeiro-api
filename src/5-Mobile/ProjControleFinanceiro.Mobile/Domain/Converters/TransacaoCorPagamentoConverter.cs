using ProjControleFinanceiro.Mobile.Data.Models;
using System.Globalization;

namespace ProjControleFinanceiro.Mobile.Domain.Converters
{
    public class TransacaoCorPagamentoConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoModel transaction = (TransacaoModel)value;

            if (transaction == null) return Colors.Transparent;

            if (transaction.Pago)
            {
                return Color.FromArgb("#FF05B76B");
            }
            else
            {
                return Color.FromArgb("#FFC82501");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
