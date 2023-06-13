using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Extensions
{
    public static class TransacaoExtension
    {
        public static Transacao ToAddDTO(this TransacaoAddDTO value)
        {
            return new Transacao(value.ContaId, value.CartaoId, value.MetodoPagamento, value.Descricao, value.Valor, value.Data, value.TipoTransacao, value.Categoria, value.Repete, value.QtdRepeticao, value.Pago);
        }
        public static Transacao ToAddDTO2(this TransacaoAddDTO value)
        {
            return new Transacao(value.ContaId, value.MetodoPagamento, value.Descricao, value.Valor, value.Data, value.TipoTransacao, value.Categoria, value.Repete, value.QtdRepeticao, value.Pago);
        }
        public static TransacaoViewDTO ToGetDTO(this Transacao value)
        {
            return new TransacaoViewDTO
            {
                Id = value.Id,
                Conta = value.Conta.Nome,
                Cartao = value.Cartao?.Nome,
                NomeFatura = value.Fatura?.Nome,
                MetodoPagamento = value.MetodoPagamento.ToString(),
                Descricao = value.Descricao,
                Valor = value.Valor,
                Data = value.Data.ToString("dd/MM/yyyy"),
                TipoTransacao = value.TipoTransacao.ToString(),
                Categoria = value.Categoria.ToString(),
                Repete = value.Repete,
                QtdRepeticao = value.QtdRepeticao,
                Pago = value.Pago
            };
        }
    }
}