using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao;

public class TransacaoAddDto
{

    public Guid ClienteId { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    [DefaultValue("dd/MM/yyyy")]
    public string Data { get; set; }
    public TipoTransacaoEnum TipoTransacao { get; set; }
    public CategoriaEnum Categoria { get; set; }
    [DefaultValue(false)]
    public bool Pago { get; set; }
    [DefaultValue(false)]
    public bool Repete { get; set; }
    public int QtdRepeticao { get; set; }

    public TransacaoAddDto()
    {
        Descricao = string.Empty;
        Valor = 0.0;
        Data = DateTime.Now.ToString("dd/MM/yyyy");
        TipoTransacao = TipoTransacaoEnum.Despesa;
        Categoria = CategoriaEnum.Outros;
        Pago = false;
        Repete = false;
        QtdRepeticao = 0;
    }

}
