using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;

namespace ProjControleFinanceiro.Data.Repositorios
{
    public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(ContextoBase context) : base(context)
        {
        }

        public async Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro)
        {
            IQueryable<Transacao> query = _context.Transacoes.Include(t => t.Cartao).Include(t => t.Conta).Include(t => t.Fatura).Where(t => t.CartaoId == null || t.CartaoId == 0);

            if (filtro.Pago && !filtro.NaoPago)
            {
                query = query.Where(t => t.Pago);
            }
            else if (!filtro.Pago && filtro.NaoPago)
            {
                query = query.Where(t => !t.Pago);
            }

            return await query.ToListAsync();
        }
        public async Task<Transacao> ObterTransacaoPorId(int id)
        {
            Transacao objeto = await _context.Transacoes.Include(t => t.Cartao).Include(t => t.Conta).Include(t => t.Fatura).FirstOrDefaultAsync(t => t.Id == id);
            return objeto;
        }
    }
}
