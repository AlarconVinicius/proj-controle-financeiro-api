using ProjControleFinanceiro.Domain.DTOs.Transacao;

namespace ProjControleFinanceiro.Domain.Interfaces.Services;

public interface ITransacaoService : IMainService
{
    public Task<TransacaoViewDto> AdicionarTransacao(TransacaoAddDto objeto);
    public Task<TransacaoViewListDto> ObterTransacoes();
    public Task<TransacaoViewListDto> ObterTransacoesMesAno(int mes, int ano);
    public Task<TransacaoViewDto> ObterTransacaoPorId(Guid id);
    public Task<bool> AtualizarTransacao(TransacaoUpdDto objeto);
    public Task<bool> AtualizarStatusPagamento(Guid id, bool pago);
    public Task<bool> DeletarTransacao(Guid id);
}
