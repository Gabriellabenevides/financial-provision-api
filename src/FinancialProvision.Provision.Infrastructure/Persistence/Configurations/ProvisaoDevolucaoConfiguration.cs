using FinancialProvision.Provision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialProvision.Provision.Infrastructure.Persistence.Configurations
{
    public class ProvisaoDevolucaoConfiguration : IEntityTypeConfiguration<ProvisaoDevolucao>
    {
        public void Configure(EntityTypeBuilder<ProvisaoDevolucao> builder)
        {
            builder.ToTable("ProvisoesDevolucao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Mes)
                .IsRequired();

            builder.Property(p => p.Ano)
                .IsRequired();

            builder.Property(p => p.ValorPrevisto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.ValorUtilizado)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasMaxLength(200);

            builder.Property(p => p.DataCriacao)
                .IsRequired();
        }
    }
}