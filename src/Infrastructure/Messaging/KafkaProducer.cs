using WarehouseSystem.Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace WarehouseSystem.Infrastructure.Messaging;

public class KafkaProducer : IKafkaProducer, IDisposable
{

    private readonly IProducer<Null,string> _producer;
    private readonly string _topic="stock-low-alerts";

    public KafkaProducer(IConfiguration configuration)
    {
        var bootstrapServers = configuration["KafkaSettings:BootstrapServers"] ?? "localhost:9092";

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            AllowAutoCreateTopics = true,
            // Osiguravamo da poruka stigne (Acks.All)
            Acks = Acks.All
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task SendAlertAsync(string topic, object message)
    {
        try 
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<Null, string> { Value = jsonMessage };

            var result = await _producer.ProduceAsync(topic, kafkaMessage);
            Console.WriteLine($"[KAFKA] Poruka isporučena na offset: {result.Offset} u particiju: {result.Partition}");
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"[KAFKA ERROR] Greška pri slanju: {e.Error.Reason}");
            throw; // Da bi API vratio 500 grešku ako Kafka padne
        }
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}