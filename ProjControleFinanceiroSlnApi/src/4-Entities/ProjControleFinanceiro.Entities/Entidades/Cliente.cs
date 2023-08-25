using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades;

[Table("Clientes")]
public class Cliente : Entity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public IEnumerable<Transacao> Transacoes { get; set; } = new List<Transacao>();

    public Cliente() { }
    public Cliente(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
    }
}
