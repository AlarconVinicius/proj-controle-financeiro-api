using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;

namespace ProjControleFinanceiro.Data.Repositorios
{
    public class CartaoRepository : BaseRepository<Cartao>, ICartaoRepository
    {
        private readonly IFaturaRepository _faturaRepository;
        public CartaoRepository(ContextoBase context, IFaturaRepository faturaRepository) : base(context)
        {
            _faturaRepository = faturaRepository;
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
        public async Task DeletarCartao(int id)
        {
            var cartao = await _context.Cartoes
            .Include(c => c.Faturas)
            .FirstOrDefaultAsync(c => c.Id == id);
            foreach (var fatura in cartao.Faturas.ToList())
            {
                await _faturaRepository.DeletarFatura(fatura.Id);
            }

            _context.Cartoes.Remove(cartao);
            await _context.SaveChangesAsync();
        }
    }
}
