using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class TipoTransacaoExtension
    {
        public static TipoTransacaoDTO ToGetDTO(this TipoTransacao value)
        {
            return new TipoTransacaoDTO
            {
                Id = value.GetHashCode(),
                TipoTransacao = value.ToString()
            };
        }
    }
}
