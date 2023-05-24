
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Repositorios
{
    public interface ICartaoRepository : IBaseRepository<Cartao>
    {
        Task<IEnumerable<Cartao>> ObterCartoes();
        Task<Cartao> ObterCartaoPorId(int id);
    }
}
