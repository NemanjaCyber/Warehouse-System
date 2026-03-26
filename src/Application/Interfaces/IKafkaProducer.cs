namespace WarehouseSystem.Application.Interfaces;

public interface IKafkaProducer
{
    Task SendAlertAsync(string topic, object message);
}