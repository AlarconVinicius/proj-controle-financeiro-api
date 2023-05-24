using ProjOrganizze.Api.Dominio.Entidades.Enums;

namespace ProjOrganizze.Api.Dominio.DTOs.Conta
{
    public class ContaUpdDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
    }
}
