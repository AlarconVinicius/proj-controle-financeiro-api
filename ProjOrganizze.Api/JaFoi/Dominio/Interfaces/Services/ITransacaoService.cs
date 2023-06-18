using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Filtros;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface ITransacaoService
    {
        public Task<Transacao> AdicionarTransacao(Transacao objeto);
        public Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro);
        public Task<Transacao> ObterTransacaoPorId(int id);
    }
}
