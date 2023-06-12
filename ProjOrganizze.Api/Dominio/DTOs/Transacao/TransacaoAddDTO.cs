using ProjOrganizze.Api.Dominio.Entidades.Enums;

namespace ProjOrganizze.Api.Dominio.DTOs.Transacao
{
    public class TransacaoAddDTO
    {
        public int ContaId { get; set; }
        public int CartaoId { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
        public bool Repete { get; set; }
        public int QtdRepeticao { get; set; }
        public bool Pago { get; set; }
    }
}
