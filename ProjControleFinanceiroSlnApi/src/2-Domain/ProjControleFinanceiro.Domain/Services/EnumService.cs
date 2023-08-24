using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Services;

public class EnumService : MainService, IEnumService
{
    public List<CategoriaDto> ObterCategoriasEnum()
    {
        return Enum.GetValues(typeof(CategoriaEnum))
                   .Cast<CategoriaEnum>()
                   .Select(categoria => categoria.ToGetDTO())
                   .ToList();
    }

    public List<TipoTransacaoDto> ObterTiposTransacaoEnum()
    {
        return Enum.GetValues(typeof(TipoTransacaoEnum))
                   .Cast<TipoTransacaoEnum>()
                   .Select(tipoTransacao => tipoTransacao.ToGetDTO())
                   .ToList();
    }
}
