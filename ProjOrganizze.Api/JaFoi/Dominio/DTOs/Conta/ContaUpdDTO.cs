using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Entidades.Enums;
using System.ComponentModel;

namespace ProjOrganizze.Api.Dominio.DTOs.Conta
{
    public class ContaUpdDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
    }

}
