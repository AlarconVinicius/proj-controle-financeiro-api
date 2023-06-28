using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoAddDTO
    {
        public string Descricao { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
    }
}
