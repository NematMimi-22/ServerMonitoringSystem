using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace ServerMonitoringSystem.RabbitMQMessage
{
    public class RabbitMQMessageQueue : IMessageQueue
    {
        private readonly IRabbitMQConfiguration _rabbitMQConfiguration;

        public RabbitMQMessageQueue(IRabbitMQConfiguration rabbitMQConfiguration)
        {
            _rabbitMQConfiguration = rabbitMQConfiguration;
        }

        public void Publish<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfiguration.HostName,
                UserName = _rabbitMQConfiguration.UserName,
                Password = _rabbitMQConfiguration.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQConfiguration.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var jsonMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);
                channel.BasicPublish(exchange: "",
                                     routingKey: _rabbitMQConfiguration.QueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Sent: {jsonMessage}");
            }
        }
    }
}