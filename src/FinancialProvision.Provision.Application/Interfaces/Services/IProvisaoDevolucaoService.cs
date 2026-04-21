using FinancialProvision.Provision.Application.DTOs;

namespace FinancialProvision.Provision.Application.Interfaces.Services;

public interface IProvisaoDevolucaoService
{
    Task<ProvisaoDevolucaoDto> CriarAsync(ProvisaoDevolucaoDto dto);
    Task AtualizarAsync(ProvisaoDevolucaoDto dto);
    Task<ProvisaoDevolucaoDto?> ObterPorIdAsync(int id);
    Task<ProvisaoDevolucaoDto?> ObterPorMesAnoAsync(int mes, int ano);
    Task<IEnumerable<ProvisaoDevolucaoDto>> ObterTodosAsync();
    Task RemoverAsync(int id);
}