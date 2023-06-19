namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoViewDTO
    {
        public int Id { get; set; }
        public string Conta { get; set; }
        public string Cartao { get; set; }
        public string NomeFatura { get; set; }
        public string MetodoPagamento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
        public string TipoTransacao { get; set; }
        public string Categoria { get; set; }
        public bool Repete { get; set; }
        public int? QtdRepeticao { get; set; }
        public bool Pago { get; set; }
    }
}
