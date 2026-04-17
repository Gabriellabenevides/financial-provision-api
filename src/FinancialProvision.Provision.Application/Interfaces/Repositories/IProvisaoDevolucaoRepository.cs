using FinancialProvision.Provision.Application.Entities;

namespace FinancialProvision.Provision.Application.Interfaces.Repositories
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
