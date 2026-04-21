using FinancialProvision.Provision.Application.Interfaces;
using FinancialProvision.Provision.Application.Interfaces.Repositories;
using FinancialProvision.Provision.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialProvision.Provision.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProvisaoDevolucaoRepository, ProvisaoDevolucaoRepository>();
        services.AddScoped<IMovimentacaoProvisaoRepository, MovimentacaoProvisaoRepository>();

        return services;
    }
}