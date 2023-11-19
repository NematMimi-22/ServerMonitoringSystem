using Microsoft.Extensions.Configuration;

namespace Weather_Monitoring.ReadConfig
{
    public class ConfigReader : IConfigurationReader
    {
        public ServerStatisticsConfig ReadConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var ServerIdentifier = configuration["ServerStatisticsConfig:ServerIdentifier"];
            var SamplingIntervalSeconds = int.Parse(configuration["ServerStatisticsConfig:SamplingIntervalSeconds"]);

            return new ServerStatisticsConfig
            {
                samplingIntervalSeconds = SamplingIntervalSeconds,
                serverIdentifier = ServerIdentifier
            };
        }

        public RabbitMQConfiguration ReadRabbitMQConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return new RabbitMQConfiguration
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                QueueName = configuration["RabbitMQ:QueueName"]
            };
        }
    }
}