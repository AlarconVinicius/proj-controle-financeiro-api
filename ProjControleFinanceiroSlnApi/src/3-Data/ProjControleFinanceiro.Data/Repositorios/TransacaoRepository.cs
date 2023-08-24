using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
{
    private readonly IUser _user;
    public TransacaoRepository(ContextoBase context, IUser user) : base(context)
    {
        _user = user;
    }

    public async Task<List<Transacao>> ObterTransacoes()
    {
        return await _context.Transacoes.Where(t => t.ClienteId == _user.GetUserId()).ToListAsync();
    }
    public async Task<List<Transacao>> ObterTransacoesMesAno(int mes, int ano)
    {
        return await _context.Transacoes
                             .Where(t => t.ClienteId == _user.GetUserId() && t.Data.Month == mes && t.Data.Year == ano)
                             .ToListAsync();
    }
    public async Task<Transacao> ObterTransacaoPorId(Guid id)
    {
        return await _context.Transacoes
                             .FirstOrDefaultAsync(t => t.ClienteId == _user.GetUserId() && t.Id == id);
    }
}
