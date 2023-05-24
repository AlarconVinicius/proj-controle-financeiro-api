namespace ProjOrganizze.Api.Dominio.DTOs.Conta
{
    public class ContaViewDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoConta { get; set; }
        public double Saldo { get; set; }
    }
}
