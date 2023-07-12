using LiteDB;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Mobile.Interfaces;

namespace ProjControleFinanceiro.Mobile.Repositories
{
    public class TransacaoRepositoy : ITransacaoRepository
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transacoes";

        public TransacaoRepositoy(LiteDatabase database)
        {
            _database = database;
        }
        public void Add(Transacao objeto)
        {
            var coll = _database.GetCollection<Transacao>(_collectionName);
            coll.Insert(objeto);
            coll.EnsureIndex(t => t.Data);
        }

        public void Delete(Transacao objeto)
        {
            var coll = _database.GetCollection<Transacao>(_collectionName);
            coll.Delete(objeto.Id);
        }

        public List<Transacao> GetAll()
        {
            return _database.GetCollection<Transacao>(_collectionName).Query().OrderByDescending(t => t.Data).ToList();
        }

        public void Update(Transacao objeto)
        {
            var coll = _database.GetCollection<Transacao>(_collectionName);
            coll.Update(objeto);
        }
    }
}
