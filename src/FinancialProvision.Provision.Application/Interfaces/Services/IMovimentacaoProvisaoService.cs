using FinancialProvision.Provision.Application.DTOs;

namespace FinancialProvision.Provision.Application.Interfaces.Services;

public interface IMovimentacaoProvisaoService
{
    Task CreateAsync(CreateMovimentacaoDto dto);

    Task<IEnumerable<ReadMovimentacaoDto>> GetByProvisaoIdAsync(int provisaoId);
}
