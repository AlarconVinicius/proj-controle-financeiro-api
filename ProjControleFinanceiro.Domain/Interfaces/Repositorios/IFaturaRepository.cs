using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface IFaturaRepository : IBaseRepository<Fatura>
    {
        Task AdicionarFaturas(Cartao objeto);
        Task<List<Fatura>> ObterFaturas(int cartaoId); // Ajustar para, ObterFaturasPorCartaoId
        Task<Fatura> ObterFaturaPorId(int faturaId, int cartaoId = 0);
        Task<Fatura> ObterFaturaPorCartaoMesAno(int cartaoId, int mes, int ano);
        Task<bool> PagarFatura(int cartaoId, int faturaId);
        Task DeletarFatura(int id);

    }
}
