﻿using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades
{
    [Table("Faturas")]
    public class Fatura
    {
        public int Id { get; set; }
        [ForeignKey("Cartao")]
        public int CartaoId { get; set; }
        public string Nome { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataFechamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public List<Transacao> Transacoes { get; private set; }

        public Cartao? Cartao { get; set; }

        public Fatura()
        {
            Transacoes = new List<Transacao>();
        }
        public Fatura(int id, int cartaoId, string nome, int mes, int ano, DateTime dataVencimento, DateTime dataFechamento, List<Transacao> transacoes, StatusPagamento statusPagamento = StatusPagamento.Pendente)
        {
            Id = id;
            CartaoId = cartaoId;
            Nome = nome;
            Mes = mes;
            Ano = ano;
            DataVencimento = dataVencimento;
            DataFechamento = dataFechamento;
            Transacoes = transacoes;
            StatusPagamento = statusPagamento;
        }
        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
        public void PagarFatura()
        {
            foreach (var transacao in Transacoes)
            {
                transacao.Pago = true;
            }
        }
    }
}
