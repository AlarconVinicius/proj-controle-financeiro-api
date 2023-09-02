using Microsoft.AspNetCore.Http;

using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Helpers;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class RelatorioRepository : BaseRepository<Transacao>, IRelatorioRepository
{
    private readonly IHttpContextAccessor _accessor;
    public RelatorioRepository(ContextoBase context, IHttpContextAccessor accessor) : base(context)
    {
        _accessor = accessor;
    }
    public IQueryable<Transacao> ObterTransacaoPeriodo(string? dtI, string? dtF)
    {
        DateTime dtInicio = dtI!.ToDateTime();
        DateTime dtFim = dtF!.ToDateTime();
        return _context.Transacoes.Where(t => t.Data.Date >= dtInicio && t.Data.Date <= dtFim && t.ClienteId == UsuarioHelper.GetUserId(_accessor)).OrderBy(x => x.Data);
    }

    public IQueryable<Transacao> ObterTransacaoPorMes(int? mes, int? ano)
    {
        return _context.Transacoes.Where(t => t.Data.Month.Equals(mes) && t.Data.Year.Equals(ano) && t.ClienteId.Equals(UsuarioHelper.GetUserId(_accessor)));
    }

    public IQueryable<Transacao> ObterTransacaoPorAno(int? ano)
    {
        return _context.Transacoes.Where(t => t.Data.Year.Equals(ano) && t.ClienteId.Equals(UsuarioHelper.GetUserId(_accessor))).OrderBy(x => x.Data);
    }
}
