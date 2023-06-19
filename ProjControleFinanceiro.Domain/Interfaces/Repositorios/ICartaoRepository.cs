using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios
{
    public interface ICartaoRepository : IBaseRepository<Cartao>
    {
        Task<IEnumerable<Cartao>> ObterCartoes();
        Task<Cartao> ObterCartaoPorId(int id);
        Task DeletarCartao(int id);
    }
}
