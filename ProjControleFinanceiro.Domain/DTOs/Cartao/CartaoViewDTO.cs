using ProjControleFinanceiro.Domain.DTOs.Fatura;

namespace ProjControleFinanceiro.Domain.DTOs.Cartao
{
    public class CartaoViewDTO
    {

        public int Id { get; set; }
        public string Conta { get; set; }
        public string Nome { get; set; }
        public double Limite { get; set; }
        public double Saldo { get; set; }
        public int VencimentoDia { get; set; }
        public int DiferencaDias { get; set; }
        public List<FaturaViewDTO> Faturas { get; set; }
    }
}
