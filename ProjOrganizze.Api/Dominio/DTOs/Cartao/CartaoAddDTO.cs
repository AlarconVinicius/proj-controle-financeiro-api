using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjOrganizze.Api.Dominio.DTOs.Cartao
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