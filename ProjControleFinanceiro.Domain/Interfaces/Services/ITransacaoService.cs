using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface ITransacaoService : IMainService
    {
        public Task<TransacaoViewDTO> AdicionarTransacao(TransacaoAddDTO objeto);
        public Task<IEnumerable<TransacaoViewDTO>> ObterTransacoes(TransacaoFiltro filtro);
        public Task<TransacaoViewDTO> ObterTransacaoPorId(int id);
    }
}
