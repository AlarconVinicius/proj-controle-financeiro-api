using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Data.Seeds.Configuracao;
public class CreateInitialTransactions
{
    private readonly ContextoBase _contextBase;
    public CreateInitialTransactions(ContextoBase contextBase)
    {
        _contextBase = contextBase;
    }
    public void Create()
    {
        Guid usuarioId = Guid.Parse("BBDC94BA-D192-409B-A9BF-BF5977B30425");
        var list = _contextBase.Transacoes.Where(l => l.ClienteId == usuarioId).ToList();
        if(list.Count <= 0)
        {
        var clienteId = usuarioId;
        var descricaoBase = "Transação Automática";
        var dataBase = DateTime.Now.AddDays(-20);
        var categoria = CategoriaEnum.Outros;
        var transacoes = new List<Transacao>();
        for (int i = 1; i <= 30; i++)
        {
            var tipoTransacao = (i % 2 == 0) ? TipoTransacaoEnum.Receita : TipoTransacaoEnum.Despesa;
            var valor = (i % 2 == 0) ? 150.00 : 100.00;

            transacoes.Add(new Transacao(clienteId, $"{descricaoBase} {i}", valor, dataBase.AddDays(i), tipoTransacao, categoria));
        }

        _contextBase.Transacoes.AddRange(transacoes);
        _contextBase.SaveChanges();
        }
    }
}
