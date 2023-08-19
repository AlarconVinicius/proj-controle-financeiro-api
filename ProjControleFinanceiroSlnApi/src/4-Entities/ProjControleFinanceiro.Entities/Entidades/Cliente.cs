namespace ProjControleFinanceiro.Entities.Entidades;

public class Cliente : Entity
{

    //EF 
    public IEnumerable<Transacao> transacoes { get; set; }
    public Cliente(){ }


}
