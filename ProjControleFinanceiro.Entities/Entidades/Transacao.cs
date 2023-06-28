using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades
{
    [Table("Transacoes")]
    public class Transacao : Entity
    {
        public MetodoPagamento MetodoPagamento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
        public Transacao(MetodoPagamento metodoPagamento, string descricao, double valor, DateTime data, TipoTransacao tipoTransacao, Categoria categoria)
        {
            MetodoPagamento = metodoPagamento;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            TipoTransacao = tipoTransacao;
            Categoria = categoria;
        }
    }
}
