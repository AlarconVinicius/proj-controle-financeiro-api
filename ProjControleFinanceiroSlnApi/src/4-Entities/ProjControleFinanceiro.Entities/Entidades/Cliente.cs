using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades;

[Table("Clientes")]
public class Cliente : Entity
{
    public IEnumerable<Transacao> Transacoes { get; set; } = new List<Transacao>();
    public Cliente(){ }


}
