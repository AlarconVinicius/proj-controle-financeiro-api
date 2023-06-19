using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface ITransacaoRepository : IBaseRepository<Transacao>
    {
        Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro);
        Task<Transacao> ObterTransacaoPorId(int id);
    }
}
