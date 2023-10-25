using RabbitMQ.Client;
using System.Text;
namespace ServerMonitoringSystem.RabbitMQMessage
{
    public class RabbitMQMessageQueue : IMessageQueue
    {
        private readonly string _hostName;
        private readonly string _queueName;

        public RabbitMQMessageQueue(string hostName, string queueName)
        {
            _hostName = hostName;
            _queueName = queueName;
        }

        public void Publish(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Sent: {message}");
            }
        }
    }
}