using Microsoft.EntityFrameworkCore;
using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Filtros;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using System.Globalization;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class FaturaRepository : BaseRepository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(ContextoBase context) : base(context)
        {
        }
        public async Task AdicionarFaturas(Cartao objeto)
        {
            int ano = DateTime.Now.Year;
            for (int mes = 1; mes <= 12; mes++)
            {
                var nomeMes = new DateTime(ano, mes, 1).ToString("MMM", CultureInfo.GetCultureInfo("pt-BR"));
                var nomeFatura = $"{nomeMes}/{ano}";
                var fatura = await _context.Faturas.FirstOrDefaultAsync(f => f.Mes == mes && f.Ano == ano && f.CartaoId == objeto.Id);

                if (fatura == null)
                {
                    fatura = new Fatura
                    {
                        CartaoId = objeto.Id,
                        Nome = nomeFatura,
                        Mes = mes,
                        Ano = ano,
                        DataVencimento = new DateTime(ano, mes, objeto.DiaVencimento),
                        DataFechamento = new DateTime(ano, mes, objeto.DiaVencimento).AddDays(-objeto.DiferencaDias)
                    };

                    _context.Faturas.Add(fatura);
                }
                else
                {
                    fatura.Nome = nomeFatura;
                    fatura.DataVencimento = new DateTime(ano, mes, objeto.DiaVencimento);
                    fatura.DataFechamento = new DateTime(ano, mes, objeto.DiaVencimento).AddDays(-objeto.DiferencaDias);
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<Fatura>> ObterFaturas()
        {
            return await _context.Faturas
                .Include(f => f.Cartao)
                .Include(f => f.Transacoes).ThenInclude(f => f.Conta)
                .ToListAsync();
        }

        public async Task<Fatura> ObterFaturaPorCartaoMesAno(int cartaoId, int mes, int ano)
        {
            if (cartaoId != 0 && mes != 0 && ano != 0)
            {
                var fatura = await _context.Faturas.Include(f => f.Cartao).Include(f => f.Transacoes).ThenInclude(f => f.Conta).FirstOrDefaultAsync(f => f.CartaoId == cartaoId && f.Mes == mes && f.Ano == ano);
                return fatura;
            }
            throw new ArgumentNullException("Informe os parâmetros corretamente.");
        }

        public async Task DeletarFatura(int id)
        {
            var fatura = await _context.Faturas
            .Include(f => f.Transacoes)
            .FirstOrDefaultAsync(f => f.Id == id);
            foreach (var transacao in fatura.Transacoes.ToList())
            {
                _context.Transacoes.Remove(transacao);
            }

            _context.Faturas.Remove(fatura);
        }
    }
}