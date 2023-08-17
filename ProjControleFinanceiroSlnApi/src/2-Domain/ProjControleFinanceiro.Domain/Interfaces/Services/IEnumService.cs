using ProjControleFinanceiro.Domain.DTOs.Enums;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface IEnumService : IMainService
    {
        public List<CategoriaDTO> ObterCategoriasEnum();
        public List<TipoTransacaoDTO> ObterTiposTransacaoEnum();
    }
}
