using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Filtros;

namespace ProjOrganizze.Api.Dominio.Interfaces.Repositorios
{
    public interface ITransacaoRepository : IBaseRepository<Transacao>
    {
        Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro);
        Task<Transacao> ObterTransacaoPorId(int id);
    }
}
