using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class MetodoPagamentoExtension
    {
        public static MetodoPagamentoDTO ToGetDTO(this MetodoPagamento value)
        {
            return new MetodoPagamentoDTO
            {
                Id = value.GetHashCode(),
                MetodoPagamento = value.ToString()
            };
        }
    }
}
