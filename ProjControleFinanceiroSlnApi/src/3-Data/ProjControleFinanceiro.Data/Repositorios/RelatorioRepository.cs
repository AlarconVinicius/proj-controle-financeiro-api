using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class RelatorioRepository : BaseRepository<Transacao>, IRelatorioRepository
{
    private readonly IUser _user;
    public RelatorioRepository(ContextoBase context, IUser user) : base(context)
    {
        _user = user;
    }
    public IQueryable<Transacao> ObterTransacaoPeriodo(string? dtI, string? dtF)
    {
        DateTime dtInicio = dtI!.ToDateTime();
        DateTime dtFim = dtF!.ToDateTime();
        return _context.Transacoes.Where(t => t.Data.Date >= dtInicio && t.Data.Date <= dtFim && t.ClienteId == _user.GetUserId()).OrderBy(x => x.Data);
    }

    public IQueryable<Transacao> ObterTransacaoPorMes(int? mes, int? ano)
    {
        return _context.Transacoes.Where(t => t.Data.Month.Equals(mes) && t.Data.Year.Equals(ano) && t.ClienteId.Equals(_user.GetUserId()));
    }

    public IQueryable<Transacao> ObterTransacaoPorAno(int? ano)
    {
        return _context.Transacoes.Where(t => t.Data.Year.Equals(ano) && t.ClienteId.Equals(_user.GetUserId())).OrderBy(x => x.Data);
    }
}
