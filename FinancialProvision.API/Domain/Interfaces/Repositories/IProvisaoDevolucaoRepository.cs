using FinancialProvision.Provision.Domain.Entities;

namespace FinancialProvision.Provision.Domain.Interfaces.Repositories
{
    public interface IProvisaoDevolucaoRepository
    {
        Task AddAsync(ProvisaoDevolucao provisao);

        Task UpdateAsync(ProvisaoDevolucao provisao);

        Task<ProvisaoDevolucao?> GetByIdAsync(int id);

        Task<ProvisaoDevolucao?> GetByMesAnoAsync(int mes, int ano);

        Task<IEnumerable<ProvisaoDevolucao>> GetAllAsync();
    }
}
