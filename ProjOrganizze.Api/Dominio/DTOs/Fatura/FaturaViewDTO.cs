using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjOrganizze.Api.Dominio.DTOs.Fatura
{
    public class FaturaViewDTO
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string Nome { get; set; }
        public string Cartao { get; set; }
        public string DataVencimento { get; set; }
        public string DataFechamento { get; set; }
        public List<TransacaoViewDTO> Transacoes { get; set; }
    }
}
