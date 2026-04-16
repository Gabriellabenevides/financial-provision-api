using FinancialProvision.Provision.Domain.Entities;
using FinancialProvision.Provision.Domain.Interfaces;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.Provision.Infrastructure.Persistence.Repositories;

public class MovimentacaoProvisaoRepository : IMovimentacaoProvisaoRepository
{
    private readonly FinancialProvisionDbContext _context;

    public MovimentacaoProvisaoRepository(
        FinancialProvisionDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovimentacaoProvisao>> GetByProvisaoIdAsync(int provisaoId)
    {
        return await _context.MovimentacoesProvisao
            .Where(x => x.ProvisaoDevolucaoId == provisaoId)
            .ToListAsync();
    }

    public async Task AddAsync(MovimentacaoProvisao entity)
    {
        await _context.MovimentacoesProvisao.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}
