﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio
{
    public class RelatorioPDF
    {
        [DefaultValue("dd/MM/yyyy")]
        public string? De { get; set; }

        [DefaultValue("dd/MM/yyyy")]
        public string? Ate { get; set; }

        [DefaultValue("Entre 1 e 12")]
        public int? Mes { get; set; }

        [DefaultValue("yyyy")]
        public int? Ano { get; set; }

        [Required]
        public CicloPDf CicloPDf { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CicloPDf
    {
        [EnumMember(Value = "Período")]
        Periodo = 1,
        [EnumMember(Value = "Mensal")]
        Mensal,
        [EnumMember(Value = "Anual")]
        Anual,
    }


}
