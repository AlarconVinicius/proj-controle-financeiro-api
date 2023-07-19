using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface ITransacaoRepository : IBaseRepository<Transacao>
    {
        Task<List<Transacao>> ObterTransacoes();
        Task<List<Transacao>> ObterTransacoesMesAno(int mes, int ano);
        Task<Transacao> ObterTransacaoPorId(int id);
    }
}
