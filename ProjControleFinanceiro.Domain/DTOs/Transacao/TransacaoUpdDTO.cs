using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoUpdDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        [DefaultValue("dd/MM/yyyy")]
        public string Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
    }
}
