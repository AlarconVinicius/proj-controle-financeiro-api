using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface IRelatorioService : IMainService
    {
        public Task<Byte[]> GerarRelatorio(RelatorioPDF query);
    }
}
