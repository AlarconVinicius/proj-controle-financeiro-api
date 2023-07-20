namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoViewListDTO
    {
        public double Entrada { get; set; }
        public double Saida { get; set; }
        public double Saldo { get; set; }
        public IEnumerable<TransacaoViewDTO> Transacoes { get; set; }

        public TransacaoViewListDTO()
        {
            Transacoes= Enumerable.Empty<TransacaoViewDTO>();
        }
    }
}
