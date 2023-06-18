using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface ICartaoService
    {
        public Task AdicionarCartao(Cartao objeto);

        public Task<Cartao> ObterCartaoPorId(int id);
        public Task<IEnumerable<Cartao>> ObterCartoes();
        public Task<Cartao> AtualizarCartao(Cartao objeto);
        public Task DeletarCartao(int id);
    }
}
