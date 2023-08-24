using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Configuracao.EntityConfigs;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Descricao).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Valor).IsRequired().HasDefaultValue(0.0).HasColumnType("decimal(18, 2)");
        builder.Property(e => e.Data).IsRequired();
        builder.Property(e => e.TipoTransacao).IsRequired().HasConversion<int>();
        builder.Property(e => e.Categoria).IsRequired().HasConversion<int>();
        builder.Property(e => e.Pago).IsRequired().HasDefaultValue(false);
        builder.Property(e => e.Repete).IsRequired().HasDefaultValue(false);
        builder.Property(e => e.QtdRepeticao).IsRequired().HasDefaultValue(0);

        builder.HasOne(e => e.Cliente)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t => t.HasCheckConstraint("CK_Valor_NaoNegativo", "Valor >= 0"));
        builder.ToTable(t => t.HasCheckConstraint("CK_TipoTransacao_NaoZero", "TipoTransacao > 0"));
        builder.ToTable(t => t.HasCheckConstraint("CK_Categoria_NaoZero", "Categoria > 0"));
    }
}