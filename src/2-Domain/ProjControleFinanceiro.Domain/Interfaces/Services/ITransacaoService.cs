using ProjControleFinanceiro.Domain.DTOs.Transacao;

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
    }
}
