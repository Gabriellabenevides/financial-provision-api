using FinancialProvision.Provision.Infrastructure.Messaging.Consumers;
using Microsoft.Extensions.Hosting;

namespace FinancialProvision.Provision.Infrastructure.Workers;

public class DevolucaoAprovadaWorker : BackgroundService
{
    private readonly DevolucaoAprovadaConsumer _consumer;

    public DevolucaoAprovadaWorker(DevolucaoAprovadaConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Consumir();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
