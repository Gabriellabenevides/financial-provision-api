using System.Text;
using System.Text.Json;
using FinancialProvision.Provision.Application.Events;
using FinancialProvision.Provision.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FinancialProvision.Provision.Infrastructure.Messaging.Consumers;

public class DevolucaoAprovadaConsumer
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DevolucaoAprovadaConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Consumir()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "devolucao-aprovada",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var mensagem = Encoding.UTF8.GetString(body);

            Console.WriteLine("Evento recebido na Provision:");
            Console.WriteLine(mensagem);

            var evento = JsonSerializer.Deserialize<DevolucaoAprovadaEvent>(mensagem);

            if (evento == null)
                return;

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider
                .GetRequiredService<IProvisaoDevolucaoRepository>();

            var provisao = await repository
                .GetByMesAnoAsync(evento.Mes, evento.Ano);

            if (provisao == null)
            {
                Console.WriteLine("Provisão não encontrada");
                return;
            }

            provisao.RegistrarUtilizacao(evento.Valor);

            await repository.UpdateAsync(provisao);

            Console.WriteLine("Provisão atualizada com sucesso!");
        };

        channel.BasicConsume(
            queue: "devolucao-aprovada",
            autoAck: true,
            consumer: consumer
        );

        Console.WriteLine("Provision Consumer rodando...");
    }
}
