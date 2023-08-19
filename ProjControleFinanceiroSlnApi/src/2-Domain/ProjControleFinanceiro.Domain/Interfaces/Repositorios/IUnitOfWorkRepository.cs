namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios;

public interface IUnitOfWorkRepository
{
    public void Commit();
    public void Rollback();
}