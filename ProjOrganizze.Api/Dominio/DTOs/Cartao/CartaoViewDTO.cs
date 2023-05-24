using ProjOrganizze.Api.Dominio.DTOs.Fatura;

namespace ProjOrganizze.Api.Dominio.DTOs.Cartao
{
    public class CartaoViewDTO
    {

        public int Id { get; set; }
        public string Conta { get; set; }
        public string Nome { get; set; }
        public double Limite { get; set; }
        public double Saldo { get; set; }
        public int DiaVencimento { get; set; }
        public int DiferencaDias { get; set; }
        public List<FaturaViewDTO> Faturas { get; set; }
    }
}
