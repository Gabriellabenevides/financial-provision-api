using FinancialProvision.Provision.Domain.Entities;
using FinancialProvision.Provision.Domain.Interfaces.Repositories;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.Provision.Infrastructure.Persistence.Repositories
{
    public class ProvisaoDevolucaoRepository : IProvisaoDevolucaoRepository
    {
        private readonly FinancialProvisionDbContext _context;

        public ProvisaoDevolucaoRepository(FinancialProvisionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProvisaoDevolucao provisao)
        {
            await _context.ProvisoesDevolucao.AddAsync(provisao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProvisaoDevolucao provisao)
        {
            _context.ProvisoesDevolucao.Update(provisao);
            await _context.SaveChangesAsync();
        }

        public async Task<ProvisaoDevolucao?> GetByIdAsync(int id)
        {
            return await _context.ProvisoesDevolucao
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProvisaoDevolucao?> GetByMesAnoAsync(int mes, int ano)
        {
            return await _context.ProvisoesDevolucao
                .FirstOrDefaultAsync(p => p.Mes == mes && p.Ano == ano);
        }

        public async Task<IEnumerable<ProvisaoDevolucao>> GetAllAsync()
        {
            return await _context.ProvisoesDevolucao
                .ToListAsync();
        }
    }
}