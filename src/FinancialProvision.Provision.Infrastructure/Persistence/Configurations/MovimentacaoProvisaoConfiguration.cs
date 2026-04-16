using FinancialProvision.Provision.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.Provision.Infrastructure.Persistence.Configurations;

public class MovimentacaoProvisaoConfiguration : IEntityTypeConfiguration<MovimentacaoProvisao>
{
    public void Configure(EntityTypeBuilder<MovimentacaoProvisao> builder)
    {
        builder.ToTable("MovimentacoesProvisao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();
    }
}
