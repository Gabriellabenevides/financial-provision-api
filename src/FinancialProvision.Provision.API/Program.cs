using FinancialProvision.Provision.Application.DependencyInjection;
using FinancialProvision.Provision.Infrastructure.DependencyInjection;
using FinancialProvision.Provision.Infrastructure.Messaging.Consumers;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using FinancialProvision.Provision.Infrastructure.Workers;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.API;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString("FinancialProvisionConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("Connection string não encontrada");

        // Controllers
        builder.Services.AddControllers();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Dependency Injection
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddRepository(builder.Configuration);

        // DbContext
        if (!builder.Environment.IsEnvironment("Testing"))
        {
            builder.Services.AddDbContext<FinancialProvisionDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                ));
        }

        // Consumer via DI
        builder.Services.AddSingleton<DevolucaoAprovadaConsumer>();

        // Worker
        builder.Services.AddHostedService<DevolucaoAprovadaWorker>();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financial Provision API v1");
            c.RoutePrefix = string.Empty;
        });

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}