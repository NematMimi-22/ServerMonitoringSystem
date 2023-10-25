namespace ServerMonitoringSystem.RabbitMQMessage
{
    public interface IMessageQueue
    {
       public void Publish(string message);
    }
}