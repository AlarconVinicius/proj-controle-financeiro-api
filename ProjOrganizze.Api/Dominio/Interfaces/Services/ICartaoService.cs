using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface ICartaoService
    {
        public Task AdicionarCartao(Cartao objeto);

        public Task<Cartao> AtualizarCartao(Cartao objeto);
    }
}
