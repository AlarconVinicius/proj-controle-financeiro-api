using LiteDB;
using ProjControleFinanceiro.Mobile.Data.Models.Enum;

namespace ProjControleFinanceiro.Mobile.Data.Models
{
    public class TransacaoModel
    {
        [BsonId]
        public int Id { get; set; }
        public TransacaoTipo Type { get; set; }
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public bool Paid { get; set; }
        public bool Repete { get; set; }
        public int QtdRepete { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
