using ServerMonitoringSystem.RabbitMQMessage;
using Weather_Monitoring.ReadConfig;

class Program
{
    static async Task Main()
    {
        var rabbitMQConfiguration = new ConfigReader().ReadRabbitMQConfig();
        var messageQueue = new RabbitMQMessageQueue(rabbitMQConfiguration);
        var memoryUsageProvider = new MemoryUsageProvider();
        var cpuUsageProvider = new CpuUsageProvider();
        Console.WriteLine("Server Statistics Collection Service is running and start collecting data.");
        var statisticsCollector = new ServerStatisticsCollector(
            messageQueue,
            new ConfigReader(),
            memoryUsageProvider,
            cpuUsageProvider
        );
        await statisticsCollector.StartCollectingUntilKeyPressed();
        Console.WriteLine("Server Statistics Collection Service stopped.");
    }
}