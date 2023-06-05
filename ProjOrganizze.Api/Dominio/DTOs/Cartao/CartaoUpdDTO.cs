namespace ProjOrganizze.Api.Dominio.DTOs.Cartao
{
    public class CartaoUpdDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Limite { get; set; }
        public int VencimentoDia { get; set; }
    }
}
