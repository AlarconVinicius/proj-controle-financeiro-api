using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface IRelatorioRepository
    {
        IQueryable<Transacao> ObterTransacaoPeriodo(string? dtInicio, string? dtFim);
        IQueryable<Transacao> ObterTransacaoPorMes(int? mes, int? ano);
        IQueryable<Transacao> ObterTransacaoPorAno(int? ano);
    }
}
