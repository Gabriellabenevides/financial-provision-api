using System.Text;
using System.Text.Json;
using FinancialProvision.Provision.Application.Entities;
using FinancialProvision.Provision.Application.Events;
using FinancialProvision.Provision.Application.Interfaces;
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
            try
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);

                Console.WriteLine("Evento recebido:");
                Console.WriteLine(mensagem);

                var evento = JsonSerializer.Deserialize<DevolucaoAprovadaEvent>(mensagem);

                if (evento == null)
                {
                    Console.WriteLine("Evento inválido");
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<IProvisaoDevolucaoRepository>();

                var movimentacaoRepository = scope.ServiceProvider
                    .GetRequiredService<IMovimentacaoProvisaoRepository>();

                var provisao = await repository
                    .GetByMesAnoAsync(evento.Mes, evento.Ano);

                if (provisao == null)
                {
                    Console.WriteLine("Provisão não encontrada");
                    return;
                }

                provisao.RegistrarUtilizacao(evento.Valor);

                var movimentacao = new MovimentacaoProvisao(
                    provisao.Id,
                    evento.Valor,
                    $"Devolução aprovada ID: {evento.DevolucaoId}"
                );

                await repository.UpdateAsync(provisao);
                await movimentacaoRepository.AddAsync(movimentacao);

                Console.WriteLine("Provisão e movimentação salvas!");

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar mensagem:");
                Console.WriteLine(ex.Message);

                channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        channel.BasicConsume(
            queue: "devolucao-aprovada",
            autoAck: false,
            consumer: consumer
        );

        Console.WriteLine("Consumer rodando...");
    }
}