using FinancialProvision.Provision.Application.DependencyInjection;
using FinancialProvision.Provision.Infrastructure.DependencyInjection;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialProvision.API;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString("FinancialProvisionConnection");

        // Controllers
        builder.Services.AddControllers();

        // Swagger padrão
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Dependency Injection das camadas
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddRepository(builder.Configuration);

        // DbContext MySQL
        builder.Services.AddDbContext<FinancialProvisionDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            ));

        var app = builder.Build();

        // Swagger
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