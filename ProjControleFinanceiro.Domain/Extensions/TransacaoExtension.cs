using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class TransacaoExtension
    {
        public static Transacao ToAddDTO(this TransacaoAddDTO value)
        {
            return new Transacao(value.MetodoPagamento, value.Descricao, value.Valor, value.Data.ToDateTime(), value.TipoTransacao, value.Categoria);
        }
        public static TransacaoViewDTO ToGetDTO(this Transacao value)
        {
            return new TransacaoViewDTO
            {
                Id = value.Id,
                MetodoPagamento = value.MetodoPagamento.ToGetDTO(),
                Descricao = value.Descricao,
                Valor = value.Valor,
                Data = value.Data.ToString("dd/MM/yyyy"),
                TipoTransacao = value.TipoTransacao.ToGetDTO(),
                Categoria = value.Categoria.ToGetDTO(),
            };
        }
    }
}