using ProjControleFinanceiro.Domain.DTOs.Enums;

namespace ProjControleFinanceiro.Domain.Interfaces.Services;

public interface IEnumService : IMainService
{
    public List<CategoriaDto> ObterCategoriasEnum();
    public List<TipoTransacaoDto> ObterTiposTransacaoEnum();
}
