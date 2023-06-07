using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using System;

namespace ProjOrganizze.Api.Extensions
{
    public static class ContaExtensions
    {

        public static Conta ToConta(this ContaViewDTO obj)
        {
           return new Conta(obj.Nome, obj.TipoConta, obj.Saldo);
        }

        public static ContaViewDTO ToContaViewDTO(this Conta obj)
        {

            return new ContaViewDTO
            {
               Id = obj.Id,
               Nome = obj.Nome,
               Saldo = obj.Saldo,
               TipoConta = obj.TipoConta
            };
       
        }

    }
}
