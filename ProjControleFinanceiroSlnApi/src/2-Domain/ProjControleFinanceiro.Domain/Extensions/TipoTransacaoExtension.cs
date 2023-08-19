using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Extensions;

public static class TipoTransacaoExtension
{
    public static TipoTransacaoDto ToGetDTO(this TipoTransacao value)
    {
        return new TipoTransacaoDto
        {
            Id = value.GetHashCode(),
            TipoTransacao = value.ToString()
        };
    }
}
