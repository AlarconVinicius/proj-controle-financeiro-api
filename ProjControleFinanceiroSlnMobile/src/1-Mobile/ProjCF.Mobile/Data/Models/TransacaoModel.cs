using LiteDB;
using ProjCF.Mobile.Data.Models.Enum;

namespace ProjCF.Mobile.Data.Models
{
    public class TransacaoModel
    {
        [BsonId]
        public int Id { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public bool Pago { get; set; }
        public bool Repete { get; set; }
        public int QtdRepete { get; set; }

        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
    }
}
