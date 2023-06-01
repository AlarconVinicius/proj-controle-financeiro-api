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
            int ano = DateTime.Now.Year;
            DateTime dataVencimento = new DateTime(ano, value.VencimentoMes, value.VencimentoDia);
            DateTime dataFechamento = new DateTime(ano, value.FechamentoMes, value.FechamentoDia);
            int diferencaDias = (dataVencimento - dataFechamento).Days;
            return new Cartao(value.ContaId, value.Nome, value.Limite, value.VencimentoDia, diferencaDias, value.Limite);
        }
        public static CartaoViewDTO ToGetDTO(this Cartao value)
        {
            return new CartaoViewDTO
            {
                Id = value.Id,
                Conta = value.Conta.Nome,
                Nome = value.Nome,
                Limite = value.Limite,
                DiaVencimento = value.DiaVencimento,
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
                faturasMapeadas.Add(faturaMapping.MapToGetDTO(fatura));
            }
            return new CartaoViewDTO
            {
                Id = value.Id,
                Conta = value.Conta.Nome,
                Nome = value.Nome,
                Limite = value.Limite,
                DiaVencimento = value.DiaVencimento,
                DiferencaDias = value.DiferencaDias,
                Saldo = value.Saldo,
                Faturas = faturasMapeadas
            };
        }
    }
        
}
