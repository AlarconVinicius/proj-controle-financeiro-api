namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T objeto);
    Task UpdateAsync(T objeto);
    Task DeleteAsync(Guid id);
    Task<T> GetEntityByIdAsync(Guid id);
    Task<List<T>> ListAsync();
}
