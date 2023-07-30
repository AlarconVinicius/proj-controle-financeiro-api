using Microsoft.AspNetCore.Http;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface ITransacaoService : IMainService
    {
        public Task<TransacaoViewDTO> AdicionarTransacao(TransacaoAddDTO objeto);
        public Task<TransacaoViewListDTO> ObterTransacoes();
        public Task<TransacaoViewListDTO> ObterTransacoesMesAno(int mes, int ano);
        public Task<TransacaoViewDTO> ObterTransacaoPorId(int id);
        public Task<bool> AtualizarTransacao(TransacaoUpdDTO objeto);
        public Task<bool> AtualizarStatusPagamento(int id, bool pago);
        public Task<bool> DeletarTransacao(int id);
        public Task<Byte[]> GerarRelatorio(RelatorioPDF query);
    }
}
