using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades
{
    [Table("Transacoes")]
    public class Transacao : Entity
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
        public bool Pago { get; set; }
        public bool Repete { get; set; }
        public int QtdRepeticao { get; set; }
        public Transacao(string descricao, double valor, DateTime data, TipoTransacao tipoTransacao, Categoria categoria, bool pago = false, bool repete = false, int qtdRepeticao = 0)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            TipoTransacao = tipoTransacao;
            Categoria = categoria;
            Pago = pago;
            Repete = repete;
            QtdRepeticao = qtdRepeticao;
        }
    }
}
