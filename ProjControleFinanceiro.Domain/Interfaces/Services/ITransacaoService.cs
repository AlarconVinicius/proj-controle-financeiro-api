using ProjControleFinanceiro.Domain.DTOs.Transacao;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface ITransacaoService : IMainService
    {
        public Task<TransacaoViewDTO> AdicionarTransacao(TransacaoAddDTO objeto);
        public Task<IEnumerable<TransacaoViewDTO>> ObterTransacoes();
        public Task<TransacaoViewDTO> ObterTransacaoPorId(int id);
    }
}
