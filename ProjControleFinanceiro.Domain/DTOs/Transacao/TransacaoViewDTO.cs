using ProjControleFinanceiro.Domain.DTOs.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoViewDTO
    {
        public int Id { get; set; }
        public MetodoPagamentoDTO MetodoPagamento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
        public TipoTransacaoDTO TipoTransacao { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
