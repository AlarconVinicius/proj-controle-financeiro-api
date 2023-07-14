using LiteDB;
using ProjControleFinanceiro.Mobile.Data.Models.Enum;

namespace ProjControleFinanceiro.Mobile.Data.Models
{
    public class TransacaoModel
    {
        [BsonId]
        public int Id { get; set; }
        public TransacaoTipo Tipo { get; set; }
        public String Nome { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public bool Pago { get; set; }
        public bool Repete { get; set; }
        public int QtdRepete { get; set; }

        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
    }
}
