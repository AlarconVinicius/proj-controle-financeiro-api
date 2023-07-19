using ProjCF.Mobile.Data.Models;
using ProjCF.Mobile.Data.Models.Enum;
using System.Globalization;

namespace ProjCF.Mobile.Domain.Converters
{
    public class TransacaoValorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoModel transaction = (TransacaoModel)value;

            if (transaction == null) return string.Empty;

            if (transaction.TipoTransacao == TipoTransacao.Receita)
            {
                return transaction.Valor.ToString("C");
            }
            else
            {

                return $"- {transaction.Valor.ToString("C")}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
