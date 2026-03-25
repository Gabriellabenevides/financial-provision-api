using FinancialProvision.Provision.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.Provision.Infrastructure.Persistence.Context
{
    public class FinancialProvisionDbContext : DbContext
    {
        public FinancialProvisionDbContext(DbContextOptions<FinancialProvisionDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProvisaoDevolucao> ProvisoesDevolucao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialProvisionDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}