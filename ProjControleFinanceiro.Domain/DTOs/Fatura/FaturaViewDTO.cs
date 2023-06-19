﻿using ProjControleFinanceiro.Domain.DTOs.Transacao;

namespace ProjControleFinanceiro.Domain.DTOs.Fatura
{
    public class FaturaViewDTO
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string Nome { get; set; }
        public string Cartao { get; set; }
        public string DataVencimento { get; set; }
        public string DataFechamento { get; set; }
        public IEnumerable<TransacaoViewDTO> Transacoes { get; set; }
    }
}