using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Mapeamentos
{
    public class CartaoMapping
    {
        public Cartao MapToAddDTO(CartaoAddDTO objeto)
        {
            int ano = DateTime.Now.Year;
            DateTime dataVencimento = new DateTime(ano, objeto.VencimentoMes, objeto.VencimentoDia);
            DateTime dataFechamento = new DateTime(ano, objeto.FechamentoMes, objeto.FechamentoDia);
            int diferencaDias = (dataVencimento - dataFechamento).Days;
            return new Cartao(objeto.ContaId, objeto.Nome, objeto.Limite, objeto.VencimentoDia, diferencaDias, objeto.Limite);
        }
        public CartaoViewDTO MapToGetDTO(Cartao objeto)
        {
            var faturaMapping = new FaturaMapping();
            List<FaturaViewDTO> faturasMapeadas = new List<FaturaViewDTO>();
            foreach (var fatura in objeto.Faturas)
            {
                faturasMapeadas.Add(faturaMapping.MapToGetDTO(fatura));
            }
            return new CartaoViewDTO
            {
                Id = objeto.Id,
                Conta = objeto.Conta.Nome,
                Nome = objeto.Nome,
                Limite = objeto.Limite,
                DiaVencimento = objeto.DiaVencimento,
                DiferencaDias = objeto.DiferencaDias,
                Saldo = objeto.Saldo,
                Faturas = faturasMapeadas
            };
        }
    }
}
