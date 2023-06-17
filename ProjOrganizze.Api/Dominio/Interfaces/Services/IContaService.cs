using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface IContaService
    {
        public Task Adicionar(Conta conta);

        public Task<Conta> AtualizarConta(Conta conta);

        public Task<IEnumerable<Conta>> ListarContas();

        public Task<Conta> ContaId(int id);

        public Task DeletarConta(int id);
    }
}
