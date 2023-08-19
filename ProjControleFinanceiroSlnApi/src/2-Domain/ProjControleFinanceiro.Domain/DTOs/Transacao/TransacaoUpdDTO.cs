using System.ComponentModel;

using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao;

public class TransacaoUpdDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    [DefaultValue("dd/MM/yyyy")]
    public string Data { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public Categoria Categoria { get; set; }

    public TransacaoUpdDto()
    {
        Descricao = string.Empty;
        Valor = 0.0;
        Data = DateTime.Now.ToString("dd/MM/yyyy");
        TipoTransacao = TipoTransacao.Despesa;
        Categoria = Categoria.Outros;
    }
}
