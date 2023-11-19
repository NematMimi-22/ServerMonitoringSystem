using Newtonsoft.Json;
using ServerMonitoringSystem;
using ServerMonitoringSystem.RabbitMQMessage;

public class ServerStatisticsCollector
{
    private readonly IMessageQueue _messageQueue;
    private readonly IConfigurationReader _configReader;
    private readonly IMemoryUsageProvider _memoryUsageProvider;
    private readonly ICpuUsageProvider _cpuUsageProvider;

    public ServerStatisticsCollector(IMessageQueue messageQueue, IConfigurationReader configReader,
        IMemoryUsageProvider memoryUsageProvider, ICpuUsageProvider cpuUsageProvider)
    {
        _messageQueue = messageQueue;
        _configReader = configReader;
        _memoryUsageProvider = memoryUsageProvider;
        _cpuUsageProvider = cpuUsageProvider;
    }

    public async Task StartCollectingUntilKeyPressed()
    {
        while (Console.KeyAvailable == false)
        {
            var samplingIntervalSeconds = _configReader.ReadConfig().samplingIntervalSeconds;
            var statistics = new ServerStatistics
            {
                MemoryUsage = _memoryUsageProvider.GetMemoryUsage(),
                AvailableMemory = _memoryUsageProvider.GetAvailableMemory(),
                CpuUsage = _cpuUsageProvider.GetCpuUsage(),
                Timestamp = DateTime.UtcNow
            };

            var serverIdentifier = _configReader.ReadConfig().serverIdentifier;
            var jsonString = JsonConvert.SerializeObject(new { Statistics = statistics, ServerIdentifier = serverIdentifier });

            _messageQueue.Publish(jsonString);

            await Task.Delay(1000);
        }

        Console.WriteLine("Data collection stopped.");
    }
}