using FinancialProvision.Provision.Domain.Interfaces;
using FinancialProvision.Provision.Domain.Interfaces.Repositories;
using FinancialProvision.Provision.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialProvision.Provision.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProvisaoDevolucaoRepository, ProvisaoDevolucaoRepository>();
        services.AddScoped<IMovimentacaoProvisaoRepository, MovimentacaoProvisaoRepository>();

        return services;
    }
}