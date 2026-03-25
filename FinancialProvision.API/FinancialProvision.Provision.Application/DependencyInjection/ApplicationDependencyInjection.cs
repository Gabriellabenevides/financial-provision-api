using FinancialProvision.Provision.Application.Interfaces.Services;
using FinancialProvision.Provision.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialProvision.Provision.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProvisaoDevolucaoService, ProvisaoDevolucaoService>();

        return services;
    }
}