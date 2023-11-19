namespace ServerMonitoringSystem.RabbitMQMessage
{
    public interface IMessageQueue
    {
       public void Publish<T>(T message);
    }
}