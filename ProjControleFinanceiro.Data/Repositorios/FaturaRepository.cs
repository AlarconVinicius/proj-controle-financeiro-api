using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using System.Globalization;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.Collections.Immutable;

namespace ProjControleFinanceiro.Data.Repositorios
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


        public async Task<List<Fatura>> ObterFaturas(int cartaoId)
        {
            return await _context.Faturas
                .Where(f => f.CartaoId == cartaoId)
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
        public async Task<Fatura> ObterFaturaPorCartaoEId(int cartaoId, int faturaId)
        {
            return await _context.Faturas.Include(f => f.Cartao).Include(f => f.Transacoes).ThenInclude(f => f.Conta).Where(f => f.Id == faturaId).Where(f => f.CartaoId == cartaoId).FirstAsync();
        }
        public async Task<Fatura> ObterFaturaPorId(int faturaId, int cartaoId = 0)
        {
            var query = _context.Faturas.Include(f => f.Cartao).Include(f => f.Transacoes).ThenInclude(f => f.Conta).Where(f => f.Id == faturaId).AsQueryable();
            if (cartaoId != 0)
            {
                query = query.Where(f => f.CartaoId == cartaoId);
            }
            return await query.FirstOrDefaultAsync();
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
        public async Task<bool> PagarFatura(int cartaoId, int faturaId)
        {
            Fatura faturaDb = await ObterFaturaPorId(faturaId, cartaoId);
            if(!faturaDb.Transacoes.Any())
            {
                return false;
            }
            int contaId = faturaDb.Cartao.ContaId;
            MetodoPagamento metodoPagamento = MetodoPagamento.Debito;
            string descricao = "Fatura '" + faturaDb.Nome + "' paga.";
            double valor = 0;
            DateTime data = DateTime.Now;
            TipoTransacao tipoTransacao = TipoTransacao.Despesa;
            Categoria categoria = Categoria.CartaoDeCredito;
            foreach (var transacao in faturaDb.Transacoes)
            {
                transacao.Pago = true;
                valor += transacao.Valor;
            }
            faturaDb.StatusPagamento = StatusPagamento.Pago;
            Transacao faturaPaga = new Transacao(contaId, metodoPagamento, descricao, valor, data, tipoTransacao, categoria, false, 0, true);
            await _context.Transacoes.AddAsync(faturaPaga);
            _context.SaveChanges();
            return true;
        }
    }
}