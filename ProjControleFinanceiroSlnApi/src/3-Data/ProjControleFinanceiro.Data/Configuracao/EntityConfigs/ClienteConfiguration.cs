using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Configuracao.EntityConfigs;
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(e => e.Id);

        builder.HasMany(c => c.Transacoes)
            .WithOne(t => t.Cliente)
            .HasForeignKey(t => t.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}