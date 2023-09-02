using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Helpers;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
{
    private readonly IHttpContextAccessor _accessor;
    public TransacaoRepository(ContextoBase context, IHttpContextAccessor accessor) : base(context)
    {
        _accessor = accessor;
    }

    public async Task<List<Transacao>> ObterTransacoes()
    {
        return await _context.Transacoes.Where(t => t.ClienteId == UsuarioHelper.GetUserId(_accessor)).ToListAsync();
    }
    public async Task<List<Transacao>> ObterTransacoesMesAno(int mes, int ano)
    {
        return await _context.Transacoes
                             .Where(t => t.ClienteId == UsuarioHelper.GetUserId(_accessor) && t.Data.Month == mes && t.Data.Year == ano)
                             .ToListAsync();
    }
    public async Task<Transacao> ObterTransacaoPorId(Guid id)
    {
        return await _context.Transacoes
                             .FirstOrDefaultAsync(t => t.ClienteId == UsuarioHelper.GetUserId(_accessor) && t.Id == id);
    }
}
