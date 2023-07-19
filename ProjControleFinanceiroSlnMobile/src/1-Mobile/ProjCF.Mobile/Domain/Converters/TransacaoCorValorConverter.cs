using ProjCF.Mobile.Data.Models;
using ProjCF.Mobile.Data.Models.Enum;
using System.Globalization;

namespace ProjCF.Mobile.Domain.Converters
{
    public class TransacaoCorValorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoModel transaction = (TransacaoModel)value;

            if (transaction == null) return Colors.Black;

            if (transaction.TipoTransacao == TipoTransacao.Receita)
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