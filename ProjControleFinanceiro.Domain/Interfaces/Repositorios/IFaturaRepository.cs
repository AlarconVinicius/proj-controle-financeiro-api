using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface IFaturaRepository : IBaseRepository<Fatura>
    {
        Task<List<Fatura>> ObterFaturas();
        Task<Fatura> ObterFaturaPorCartaoMesAno(int cartaoId, int mes, int ano);
        Task AdicionarFaturas(Cartao objeto);
        Task DeletarFatura(int id);

    }
}
