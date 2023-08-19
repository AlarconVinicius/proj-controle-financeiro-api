using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions;

public static class TransacaoExtension
{
    public static Transacao ToAddDTO(this TransacaoAddDto value)
    {
        return new Transacao(value.Descricao, value.Valor, value.Data.ToDateTime(), value.TipoTransacao, value.Categoria, value.Pago, value.Repete, value.QtdRepeticao);
    }
    public static TransacaoViewDto ToGetDTO(this Transacao value)
    {
        return new TransacaoViewDto
        {
            Id = value.Id,
            Descricao = value.Descricao,
            Valor = value.Valor,
            Data = value.Data.ToString("dd/MM/yyyy"),
            TipoTransacao = value.TipoTransacao.ToGetDTO(),
            Categoria = value.Categoria.ToGetDTO(),
            Pago = value.Pago
        };
    }

    public static TransacaoViewDto ToGetDTORelatorio(this Transacao value)
    {
        return new TransacaoViewDto
        {
            Descricao = value.Descricao,
            Valor = value.Valor,
            Data = value.Data.ToString("dd/MM/yyyy"),
            PagoRelatorio = value.Pago ? "Pago" : "Não pago"
        };
    }

}