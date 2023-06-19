using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface IContaService
    {
        public Task AdicionarConta(Conta conta);

        public Task<Conta> AtualizarConta(Conta conta);

        public Task<IEnumerable<Conta>> ObterContas();

        public Task<Conta> ObterContaPorId(int id);

        public Task DeletarConta(int id);
    }
}
