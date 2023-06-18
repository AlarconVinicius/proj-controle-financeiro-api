namespace ProjControleFinanceiro.Domain.DTOs.Cartao
{
    public class CartaoAddDTO
    {
        public int ContaId { get; set; }
        public string Nome { get; set; }
        public double Limite { get; set; }
        public string VencimentoData { get; set; }
        public string FechamentoData { get; set; }
    }
}