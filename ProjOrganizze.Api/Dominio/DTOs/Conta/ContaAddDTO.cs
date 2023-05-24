using ProjOrganizze.Api.Dominio.Entidades.Enums;

namespace ProjOrganizze.Api.Dominio.DTOs.Conta
{
    public class ContaAddDTO
    {
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
    }
}
