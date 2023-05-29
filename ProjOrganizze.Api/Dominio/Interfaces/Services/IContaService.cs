using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Dominio.Interfaces.Services
{
    public interface IContaService
    {
        public Task Adicionar(Conta conta);

        public Task<Conta> AtualizarConta(Conta conta);

    }
}
