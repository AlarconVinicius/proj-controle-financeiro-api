using Microsoft.EntityFrameworkCore;
using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ContextoBase _context;

        public BaseRepository(ContextoBase context)
        {
            _context = context;
        }
        public async Task AddAsync(T objeto)
        {
            await _context.Set<T>().AddAsync(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T objeto)
        {
            _context.Set<T>().Update(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var objeto = await GetEntityByIdAsync(id);
            _context.Set<T>().Remove(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ListAsync()
        {
            //return _context.Set<T>().AsNoTracking().AsQueryable();
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

    }
}
