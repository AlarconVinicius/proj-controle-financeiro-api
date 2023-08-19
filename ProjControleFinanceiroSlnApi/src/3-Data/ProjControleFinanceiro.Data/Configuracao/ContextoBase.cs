using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Configuracao;

public class ContextoBase : DbContext
{
    public ContextoBase(DbContextOptions<ContextoBase> options) : base(options)
    { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

}
public class FornecedorMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        //1 : N
        builder.HasMany(c => c.transacoes)
            .WithOne(t => t.cliente)
            .HasForeignKey(t => t.ClienteId);

       builder.ToTable("clientes");

    }
}
