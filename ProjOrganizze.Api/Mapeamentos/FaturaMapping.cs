using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Mapeamentos
{
    public class FaturaMapping
    {
        public FaturaViewDTO MapToGetDTO(Fatura objeto)
        {
            var transacaoMapping = new TransacaoMapping();
            List<TransacaoViewDTO> transacoesMapeadas = new List<TransacaoViewDTO>();
            if(objeto.Transacoes != null)
            {
                foreach (var transacao in objeto.Transacoes)
                {
                    transacoesMapeadas.Add(transacaoMapping.MapToGetDTO(transacao));
                }
            }
            return new FaturaViewDTO
            {
                Id = objeto.Id,
                Mes = objeto.Mes,
                Ano = objeto.Ano,
                Nome = objeto.Nome,
                Cartao = objeto.Cartao != null ? objeto.Cartao.Nome : string.Empty,
                DataVencimento = objeto.DataVencimento.ToString("dd/MM/yyyy"),
                DataFechamento = objeto.DataFechamento.ToString("dd/MM/yyyy"),
                Transacoes = transacoesMapeadas
            };
        }
    }
}