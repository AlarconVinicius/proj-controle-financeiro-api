using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios
{
    public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(ContextoBase context) : base(context)
        {
        }

        public async Task<List<Transacao>> ObterTransacoes()
        {
            return await _context.Transacoes.ToListAsync();
        }
        public async Task<Transacao> ObterTransacaoPorId(int id)
        {
            return await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
