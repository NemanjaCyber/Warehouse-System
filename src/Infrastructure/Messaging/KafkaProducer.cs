using WarehouseSystem.Application.Interfaces;

namespace WarehouseSystem.Infrastructure.Messaging;

public class KafkaProducer : IKafkaProducer
{
    public async Task SendAlertAsync(string topic, object message)
    {
        // Simulacija slanja na Kafku
        Console.WriteLine($"[KAFKA ALERT] Poslato na topic {topic}: {System.Text.Json.JsonSerializer.Serialize(message)}");
        await Task.CompletedTask;
    }
}