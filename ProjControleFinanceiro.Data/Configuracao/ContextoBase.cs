using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Configuracao
{
    public class ContextoBase : DbContext
    {
        public ContextoBase(DbContextOptions<ContextoBase> options) : base(options)
        { }

        public DbSet<Transacao> Transacoes { get; set; }
    }
}
