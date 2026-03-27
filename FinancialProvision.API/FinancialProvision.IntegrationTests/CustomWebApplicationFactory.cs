using FinancialProvision.API;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FinancialProvision.IntegrationTests
{
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // remove o banco configurado no Program.cs (MySQL)
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType ==
                        typeof(DbContextOptions<FinancialProvisionDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);


                // adiciona banco em memória
                services.AddDbContext<FinancialProvisionDbContext>(options =>
                {
                    options.UseInMemoryDatabase("FinancialProvisionTestDb");
                });


                // cria o banco
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var db = scope.ServiceProvider
                    .GetRequiredService<FinancialProvisionDbContext>();

                db.Database.EnsureCreated();
            });
        }
    }
}