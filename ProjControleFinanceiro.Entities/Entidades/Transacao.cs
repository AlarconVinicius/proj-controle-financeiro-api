using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades
{
    [Table("Transacoes")]
    public class Transacao
    {
        public int Id { get; set; }
        [ForeignKey("Conta")]
        public int ContaId { get; set; }
        [ForeignKey("Cartao")]
        public int? CartaoId { get; set; }
        [ForeignKey("Fatura")]
        public int? FaturaId { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
        public bool Repete { get; set; }
        public int? QtdRepeticao { get; set; }
        public bool Pago { get; set; }

        public Conta? Conta { get; set; }
        public Cartao? Cartao { get; set; }
        public Fatura? Fatura { get; set; }
        public Transacao(int contaId, MetodoPagamento metodoPagamento, string descricao, double valor, DateTime data, TipoTransacao tipoTransacao, Categoria categoria, bool repete, int? qtdRepeticao, bool pago)
        {
            ContaId = contaId;
            MetodoPagamento = metodoPagamento;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            TipoTransacao = tipoTransacao;
            Categoria = categoria;
            Repete = repete;
            QtdRepeticao = qtdRepeticao;
            Pago = pago;
        }
        public Transacao(int contaId, int? cartaoId, MetodoPagamento metodoPagamento, string descricao, double valor, DateTime data, TipoTransacao tipoTransacao, Categoria categoria, bool repete, int? qtdRepeticao, bool pago)
        {
            ContaId = contaId;
            CartaoId = cartaoId;
            MetodoPagamento = metodoPagamento;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            TipoTransacao = tipoTransacao;
            Categoria = categoria;
            Repete = repete;
            QtdRepeticao = qtdRepeticao;
            Pago = pago;
        }
        public Transacao(int contaId, int? cartaoId, int? faturaId, MetodoPagamento metodoPagamento, string descricao, double valor, DateTime data, TipoTransacao tipoTransacao, Categoria categoria, bool repete, int? qtdRepeticao, bool pago)
        {
            ContaId = contaId;
            CartaoId = cartaoId;
            FaturaId = faturaId;
            MetodoPagamento = metodoPagamento;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            TipoTransacao = tipoTransacao;
            Categoria = categoria;
            Repete = repete;
            QtdRepeticao = qtdRepeticao;
            Pago = pago;
        }
    }
}
