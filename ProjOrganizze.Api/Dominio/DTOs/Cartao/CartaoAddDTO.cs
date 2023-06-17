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

        [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "O formato da data deve ser dd/mm/yyyy")]
        public string VencimentoData { get; set; }
        [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "O formato da data deve ser dd/mm/yyyy")]
        public string FechamentoData { get; set; }
    }
}