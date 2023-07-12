using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Mobile.Interfaces
{
    public interface ITransacaoRepository
    {
        void Add(Transacao objeto);
        void Delete(Transacao objeto);
        List<Transacao> GetAll();
        void Update(Transacao objeto);
    }
}
