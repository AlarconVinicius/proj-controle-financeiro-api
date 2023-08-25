using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;

namespace ProjControleFinanceiro.Data.Repositorios;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly ContextoBase _context;

    public UnitOfWorkRepository(ContextoBase context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
    public void Rollback()
    {
        //
    }
}
