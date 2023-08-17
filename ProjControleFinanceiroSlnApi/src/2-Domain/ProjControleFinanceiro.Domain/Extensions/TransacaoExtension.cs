using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class TransacaoExtension
    {
        public static Transacao ToAddDTO(this TransacaoAddDTO value)
        {
            return new Transacao(value.ClienteId, value.Descricao, value.Valor, value.Data.ToDateTime(), value.TipoTransacao, value.Categoria, value.Pago, value.Repete, value.QtdRepeticao);
        }
        public static TransacaoViewDTO ToGetDTO(this Transacao value)
        {
            return new TransacaoViewDTO
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

        public static TransacaoViewDTO ToGetDTORelatorio(this Transacao value)
        {
            return new TransacaoViewDTO
            {
                Descricao = value.Descricao,
                Valor = value.Valor,
                Data = value.Data.ToString("dd/MM/yyyy"),
                PagoRelatorio = value.Pago ? "Pago" : "Não pago"
            };
        }

    }
}