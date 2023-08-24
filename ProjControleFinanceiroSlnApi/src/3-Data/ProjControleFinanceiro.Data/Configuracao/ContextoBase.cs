using Microsoft.EntityFrameworkCore;

using ProjControleFinanceiro.Data.Configuracao.EntityConfigs;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Configuracao;

public class ContextoBase : DbContext
{
    public ContextoBase(DbContextOptions<ContextoBase> options) : base(options)
    { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}
