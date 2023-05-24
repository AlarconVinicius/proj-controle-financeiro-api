using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Mapeamentos
{
    public class TransacaoMapping
    {
        public TransacaoViewDTO MapToGetDTO(Transacao objeto)
        {
            if (objeto == null)
            {
                throw new Exception("Transação não encontrado.");
            }
            return new TransacaoViewDTO
            {
                Id = objeto.Id,
                Conta = objeto.Conta.Nome,
                Cartao = objeto.Cartao.Nome,
                NomeFatura = objeto.Fatura.Nome,
                MetodoPagamento = objeto.MetodoPagamento.ToString(),
                Descricao = objeto.Descricao,
                Valor = objeto.Valor,
                Data = objeto.Data.ToString("dd/MM/yyyy"),
                TipoTransacao = objeto.TipoTransacao.ToString(),
                Categoria = objeto.Categoria.ToString(),
                Repete = objeto.Repete,
                QtdRepeticao = objeto.QtdRepeticao,
                Pago = objeto.Pago
            };
        }
    }
}