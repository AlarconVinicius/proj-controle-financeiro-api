﻿using ProjControleFinanceiro.Domain.DTOs.Fatura;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class FaturaExtension
    {
        public static FaturaViewDTO ToGetDTO(this Fatura objeto)
        {
            IEnumerable<TransacaoViewDTO> transacoesMapeadas = Enumerable.Empty<TransacaoViewDTO>();
            if (objeto.Transacoes != null)
            {
                transacoesMapeadas = objeto.Transacoes.Select(x => x.ToGetDTO());
            }
            return new FaturaViewDTO
            {
                Id = objeto.Id,
                Mes = objeto.Mes,
                Ano = objeto.Ano,
                Nome = objeto.Nome,
                Cartao = objeto.Cartao != null ? objeto.Cartao.Nome : string.Empty,
                DataVencimento = objeto.DataVencimento.ToString("dd/MM/yyyy"),
                DataFechamento = objeto.DataFechamento.ToString("dd/MM/yyyy"),
                Transacoes = transacoesMapeadas
            };
        }
    }
}