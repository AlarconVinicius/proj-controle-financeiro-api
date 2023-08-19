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
        return Enum.GetValues(typeof(Categoria))
                   .Cast<Categoria>()
                   .Select(categoria => categoria.ToGetDTO())
                   .ToList();
    }

    public List<TipoTransacaoDto> ObterTiposTransacaoEnum()
    {
        return Enum.GetValues(typeof(TipoTransacao))
                   .Cast<TipoTransacao>()
                   .Select(tipoTransacao => tipoTransacao.ToGetDTO())
                   .ToList();
    }
}
