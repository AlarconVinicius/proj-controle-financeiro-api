using ProjControleFinanceiro.Domain.DTOs.Enums;
using System.ComponentModel;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoViewDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        [DefaultValue("dd/MM/yyyy")]
        public string Data { get; set; }
        public TipoTransacaoDTO TipoTransacao { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public bool Pago { get; set; }
        public string? PagoRelatorio { get; set; }
        public TransacaoViewDTO()
        {
            Descricao = string.Empty;
            Valor = 0.0;
            Data = "dd/MM/yyyy";
            TipoTransacao = null!;
            Categoria = null!;
            Pago = false;
        }
    }
}
