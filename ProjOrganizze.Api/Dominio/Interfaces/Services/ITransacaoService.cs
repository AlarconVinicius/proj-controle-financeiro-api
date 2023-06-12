using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface ITransacaoService
    {
        public Task<Transacao> AdicionarTransacao(Transacao objeto);
    }
}
