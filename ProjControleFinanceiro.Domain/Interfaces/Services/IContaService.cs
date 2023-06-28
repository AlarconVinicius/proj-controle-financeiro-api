using ProjControleFinanceiro.Domain.DTOs.Conta;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface IContaService : IMainService
    {
        public Task<ContaViewDTO> AdicionarConta(ContaAddDTO conta);

        public Task<ContaViewDTO> AtualizarConta(ContaUpdDTO conta);

        public Task<IEnumerable<ContaViewDTO>> ObterContas();

        public Task<ContaViewDTO> ObterContaPorId(int id);

        public Task DeletarConta(int id);
    }
}
