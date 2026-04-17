using FinancialProvision.Provision.Application.Entities;

namespace FinancialProvision.Provision.Application.Interfaces;

public interface IMovimentacaoProvisaoRepository
{
    Task<List<MovimentacaoProvisao>> GetByProvisaoIdAsync(int provisaoId);

    Task AddAsync(MovimentacaoProvisao entity);
}