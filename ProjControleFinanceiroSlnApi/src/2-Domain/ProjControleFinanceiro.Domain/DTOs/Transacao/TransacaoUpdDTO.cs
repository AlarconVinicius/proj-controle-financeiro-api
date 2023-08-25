using System.ComponentModel;

using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao;

public class TransacaoUpdDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    [DefaultValue("dd/MM/yyyy")]
    public string Data { get; set; }
    public TipoTransacaoEnum TipoTransacao { get; set; }
    public CategoriaEnum Categoria { get; set; }

    public TransacaoUpdDto()
    {
        Descricao = string.Empty;
        Valor = 0.0;
        Data = DateTime.Now.ToString("dd/MM/yyyy");
        TipoTransacao = TipoTransacaoEnum.Despesa;
        Categoria = CategoriaEnum.Outros;
    }
}
