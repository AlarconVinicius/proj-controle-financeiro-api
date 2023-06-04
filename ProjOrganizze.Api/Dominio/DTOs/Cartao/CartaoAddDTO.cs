using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjOrganizze.Api.Dominio.DTOs.Cartao
{
    public class CartaoAddDTO
    {
        public int ContaId { get; set; }
        [Required (ErrorMessage = "Campo Nome é obrigatório.")]
        public string Nome { get; set; }
        public double Limite { get; set; }
        public int VencimentoMes { get; set; }
        public int VencimentoDia { get; set; }
        public int FechamentoMes { get; set; }
        public int FechamentoDia { get; set; }
    }
}