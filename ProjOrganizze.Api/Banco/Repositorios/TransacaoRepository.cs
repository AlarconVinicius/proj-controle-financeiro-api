using Microsoft.EntityFrameworkCore;
using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Filtros;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(ContextoBase context) : base(context)
        {
        }

        public async Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro)
        {
            IQueryable<Transacao> query = _context.Transacoes.Include(t => t.Cartao).Include(t => t.Conta).Include(t => t.Fatura);

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
    }
}
