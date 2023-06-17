using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Mapeamentos;
using System;

namespace ProjOrganizze.Api.Extensions
{
    public static class CartaoExtension
    {
        
        public static Cartao ToAddDTO(this CartaoAddDTO value)
        {
            int diferencaDias = (value.VencimentoData.ToDateTime() - value.FechamentoData.ToDateTime()).Days;
            return new Cartao(value.ContaId, value.Nome, value.Limite, value.VencimentoData.ToDateTime().Day, diferencaDias, value.Limite);
        }

        public static Cartao ToUpdDTO(this CartaoUpdDTO value)
        {
            return new Cartao(value.Id, value.Nome, value.Limite, value.VencimentoDia);
        }

        public static CartaoViewDTO ToGetDTO(this Cartao value)
        {
            return new CartaoViewDTO
            {
                Id = value.Id,
                Conta = value.Conta.Nome,
                Nome = value.Nome,
                Limite = value.Limite,
                VencimentoDia = value.DiaVencimento,
                DiferencaDias = value.DiferencaDias,
                Saldo = value.Saldo,
                Faturas = new List<FaturaViewDTO>()
            };
        }

        public static CartaoViewDTO ToGetDetailsDTO(this Cartao value)
        {
            var faturaMapping = new FaturaMapping();
            List<FaturaViewDTO> faturasMapeadas = new List<FaturaViewDTO>();
            foreach (var fatura in value.Faturas)
            {
                faturasMapeadas.Add(fatura.ToGetDTO());
                //faturasMapeadas.Add(faturaMapping.MapToGetDTO(fatura));
            }
            return new CartaoViewDTO
            {
                Id = value.Id,
                Conta = value.Conta.Nome,
                Nome = value.Nome,
                Limite = value.Limite,
                VencimentoDia = value.DiaVencimento,
                DiferencaDias = value.DiferencaDias,
                Saldo = value.Saldo,
                Faturas = faturasMapeadas
            };
        }
    }
        
}
