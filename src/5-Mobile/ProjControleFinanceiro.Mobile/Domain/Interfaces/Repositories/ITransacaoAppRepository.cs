using ProjControleFinanceiro.Mobile.Data.Models;

namespace ProjControleFinanceiro.Mobile.Domain.Interfaces.Repositories
{
    public interface ITransacaoAppRepository
    {
        void AdicionarTransacao(TransacaoModel objeto);
        void DeletarTransacao(TransacaoModel objeto);
        TransacaoModel ObterTransacaoPorId(int id);
        List<TransacaoModel> ObterTodasTransacoes();
        IEnumerable<TransacaoModel> ObterTransacaoPorMesEAno(int month, int year);
        void AtualizarTransacao(TransacaoModel objeto);
    }
}
