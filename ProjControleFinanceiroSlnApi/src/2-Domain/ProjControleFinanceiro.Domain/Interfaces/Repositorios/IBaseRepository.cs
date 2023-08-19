namespace ProjControleFinanceiro.Domain.Interfaces.Repositorios;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T objeto);
    Task UpdateAsync(T objeto);
    Task DeleteAsync(int id);
    Task<T> GetEntityByIdAsync(int id);
    Task<List<T>> ListAsync();
}
