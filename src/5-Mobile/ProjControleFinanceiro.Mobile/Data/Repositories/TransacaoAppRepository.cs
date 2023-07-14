using LiteDB;
using ProjControleFinanceiro.Mobile.Data.Models;
using ProjControleFinanceiro.Mobile.Domain.Interfaces.Repositories;

namespace ProjControleFinanceiro.Mobile.Data.Repositories
{
    public class TransacaoAppRepository : ITransacaoAppRepository
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transactions";
        public TransacaoAppRepository(LiteDatabase database)
        {
            _database = database;
        }
        public void AdicionarTransacao(TransacaoModel objeto)
        {
            objeto.CreatedAt = DateTime.UtcNow;
            objeto.ModifiedAt = DateTime.UtcNow;
            var coll = _database.GetCollection<TransacaoModel>(_collectionName);
            coll.Insert(objeto);
            coll.EnsureIndex(t => t.Date);
        }

        public void AtualizarTransacao(TransacaoModel objeto)
        {
            objeto.ModifiedAt = DateTime.UtcNow;
            var coll = _database.GetCollection<TransacaoModel>(_collectionName);
            coll.Update(objeto);
        }

        public void DeletarTransacao(TransacaoModel objeto)
        {
            var coll = _database.GetCollection<TransacaoModel>(_collectionName);
            coll.Delete(objeto.Id);
        }

        public List<TransacaoModel> ObterTodasTransacoes()
        {
            return _database.GetCollection<TransacaoModel>(_collectionName).Query().OrderByDescending(t => t.Date).ToList();
        }

        public TransacaoModel ObterTransacaoPorId(int id)
        {
            return _database.GetCollection<TransacaoModel>(_collectionName).FindById(id);
        }

        public IEnumerable<TransacaoModel> ObterTransacaoPorMesEAno(int month, int year)
        {
            return _database.GetCollection<TransacaoModel>("transactions")
                            .Find(t => t.Date.Month == month && t.Date.Year == year)
                            .OrderByDescending(t => t.Date)
                            .ToList();
        }
    }
}
