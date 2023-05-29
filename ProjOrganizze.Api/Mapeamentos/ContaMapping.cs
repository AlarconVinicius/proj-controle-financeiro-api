using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Mapeamentos
{
    public class ContaMapping
    {
        public Conta MapToAddDTO(ContaViewDTO objeto)
        {
            return new Conta(objeto.Nome, objeto.TipoConta, objeto.Saldo);
        }
        public ContaViewDTO MapToGetDTO(Conta objeto)
        {
            return new ContaViewDTO
            {
                Id = objeto.Id,
                Nome = objeto.Nome,
                Saldo = objeto.Saldo,
                TipoConta = objeto.TipoConta
            };
        }
    }
}
