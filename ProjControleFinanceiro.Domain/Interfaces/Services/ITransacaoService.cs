using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface ITransacaoService
    {
        public Task<Transacao> AdicionarTransacao(Transacao objeto);
        public Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro);
        public Task<Transacao> ObterTransacaoPorId(int id);
    }
}
