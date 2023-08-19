using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CicloPdfDto
{
    [EnumMember(Value = "Período")]
    Periodo = 1,
    [EnumMember(Value = "Mensal")]
    Mensal,
    [EnumMember(Value = "Anual")]
    Anual,
}
