using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WarehouseSystem.Infrastructure.Messaging;

public class StockAlertConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<StockAlertConsumer> _logger;
    private readonly string _topic = "stock-low-alerts";

    public StockAlertConsumer(IConfiguration configuration, ILogger<StockAlertConsumer> logger)
    {
        _logger = logger;

        var config = new ConsumerConfig
        {
            BootstrapServers = configuration["KafkaSettings:BootstrapServers"] ?? "localhost:9092",
            GroupId = $"warehouse-group-{Guid.NewGuid()}", // Generiše potpuno novu grupu pri svakom startu
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false, // Isključujemo auto-commit da bi nas forsirao da čitamo
            FetchWaitMaxMs = 100,
            SessionTimeoutMs = 6000,
            // Forsiramo kooperativnu strategiju koja je stabilnija u Dockeru
            PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Koristimo Task.Run da potpuno izmestimo Kafku iz glavnog thread-a
        return Task.Run(() =>
        {
            _consumer.Subscribe(_topic);
            _logger.LogInformation($"[CONSUMER] Pretplatili smo se na topic: {_topic}");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Loguj svaki pokušaj čitanja (pobriši ovo kad proradi)
                    Console.WriteLine("[DEBUG] Consumer čeka poruku...");

                    // Timeout od 1 sekunde da ne "zakucamo" procesor
                    var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(2));

                    if (consumeResult != null)
                   {
                        // Koristimo direktan Console.WriteLine jer on uvek prolazi, 
                        // za slučaj da Logger ima čudne filtere
                        Console.WriteLine($"\n[!!!] NABAVKA ALERT: {consumeResult.Message.Value}");
                        _logger.LogWarning($"[CONSUMER] Poruka sa offseta {consumeResult.Offset} procesuirana.");
                        // Ručno potvrđujemo čitanje pošto smo isključili AutoCommit
                        _consumer.Commit(consumeResult);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[CONSUMER ERROR] {ex.Message}");
                }
            }
        }, stoppingToken);
    }
}