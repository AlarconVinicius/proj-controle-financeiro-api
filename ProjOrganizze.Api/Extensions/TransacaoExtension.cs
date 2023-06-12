using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Entidades.Enums;
using System.Drawing;

namespace ProjOrganizze.Api.Extensions
{
    public static class TransacaoExtension
    {
        public static Transacao ToAddDTO(this TransacaoAddDTO value)
        {
            return new Transacao(value.ContaId, value.CartaoId, value.MetodoPagamento, value.Descricao, value.Valor, value.Data, value.TipoTransacao, value.Categoria, value.Repete, value.QtdRepeticao, value.Pago);
        }
    }
}