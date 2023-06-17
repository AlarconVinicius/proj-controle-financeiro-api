using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Filtros;

namespace ProjOrganizze.Api.Dominio.Interfaces.Repositorios
{
    public interface IFaturaRepository : IBaseRepository<Fatura>
    {
        Task<List<Fatura>> ObterFaturas();
        Task<Fatura> ObterFaturaPorCartaoMesAno(int cartaoId, int mes, int ano);
        Task AdicionarFaturas(Cartao objeto);
        Task DeletarFatura(int id);

    }
}
