using FinancialProvision.Provision.Domain.Entities;

namespace FinancialProvision.Provision.Domain.Interfaces;

public interface IMovimentacaoProvisaoRepository
{
    Task<List<MovimentacaoProvisao>> GetByProvisaoIdAsync(int provisaoId);

    Task AddAsync(MovimentacaoProvisao entity);
}