using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;

namespace ProjControleFinanceiro.Domain.Interfaces.Services;

public interface ITransacaoService : IMainService
{
    public Task<TransacaoViewDto> AdicionarTransacao(TransacaoAddDto objeto);
    public Task<TransacaoViewListDto> ObterTransacoes();
    public Task<TransacaoViewListDto> ObterTransacoesMesAno(int mes, int ano);
    public Task<TransacaoViewDto> ObterTransacaoPorId(int id);
    public Task<bool> AtualizarTransacao(TransacaoUpdDto objeto);
    public Task<bool> AtualizarStatusPagamento(int id, bool pago);
    public Task<bool> DeletarTransacao(int id);
    public Task<Byte[]> GerarRelatorio(RelatorioPdfDto query);
}
