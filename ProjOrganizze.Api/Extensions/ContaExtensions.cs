using Microsoft.OpenApi.Extensions;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Extensions
{
    public static class ContaExtensions
    {

        public static Conta ToAddDTO(this ContaAddDTO value)
        {
           return new Conta(value.Nome, value.TipoConta, value.Saldo);
        }
        public static Conta ToUpdDTO(this ContaUpdDTO value)
        {
            return new Conta(value.Id, value.Nome, value.TipoConta, 0);
        }
        public static ContaViewDTO ToGetDTO(this Conta value)
        {
            return new ContaViewDTO
            {
                Id = value.Id,
                Nome = value.Nome,
                Saldo = value.Saldo,
                TipoConta = value.TipoConta.GetDisplayName(),
            };
       
        }

    }
}
