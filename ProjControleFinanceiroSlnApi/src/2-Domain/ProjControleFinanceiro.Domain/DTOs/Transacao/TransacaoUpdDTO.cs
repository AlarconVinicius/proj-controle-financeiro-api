using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel;

namespace ProjControleFinanceiro.Domain.DTOs.Transacao
{
    public class TransacaoUpdDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        [DefaultValue("dd/MM/yyyy")]
        public string Data { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }

        public TransacaoUpdDTO()
        {
            Descricao = string.Empty;
            Valor = 0.0;
            Data = DateTime.Now.ToString("dd/MM/yyyy");
            TipoTransacao = TipoTransacao.Despesa;
            Categoria = Categoria.Outros;
        }
    }
}
