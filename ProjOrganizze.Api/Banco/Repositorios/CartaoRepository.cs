using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class CartaoRepository : BaseRepository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(ContextoBase context) : base(context)
        {
        }

        public async Task<IEnumerable<Cartao>> ObterCartoes()
        {
            var query = _context.Cartoes
                .Include(ct => ct.Conta)
                .Include(ct => ct.Faturas)
                .ThenInclude(ct => ct.Transacoes)
                .AsQueryable();
            return await query.ToListAsync();
        }
        public async Task<Cartao> ObterCartaoPorId(int id)
        {
            var cartaoDb = await _context.Cartoes
                .Include(ct => ct.Conta)
                .Include(ct => ct.Faturas)
                .ThenInclude(ct => ct.Transacoes)
                .SingleOrDefaultAsync(ct => ct.Id == id);
            return cartaoDb;
        }
    }
}
