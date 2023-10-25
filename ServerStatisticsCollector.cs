using Newtonsoft.Json;
using ServerMonitoringSystem;
using ServerMonitoringSystem.RabbitMQMessage;
using System.Diagnostics;
using Weather_Monitoring.ReadConfig;
public class ServerStatisticsCollector
{
    private readonly IMessageQueue _messageQueue;

    public ServerStatisticsCollector(IMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }

    public async Task StartCollecting()
    {
        var samplingIntervalSeconds = ConfigReader.ReadConfig().samplingIntervalSeconds;
        var statistics = new ServerStatistics
        {
            MemoryUsage = GetMemoryUsage(),
            AvailableMemory = GetAvailableMemory(),
            CpuUsage = GetCpuUsage(),
            Timestamp = DateTime.UtcNow
        };
        var serverIdentifier = ConfigReader.ReadConfig().serverIdentifier;
        var jsonString = JsonConvert.SerializeObject(new { Statistics = statistics, ServerIdentifier = serverIdentifier });
        _messageQueue.Publish(jsonString);
        await Task.Delay(samplingIntervalSeconds * 1000);
    }

    private double GetMemoryUsage()
    {
        var currentProcess = Process.GetCurrentProcess();
        var memoryUsageBytes = currentProcess.WorkingSet64;
        var memoryUsageMB = memoryUsageBytes / (1024.0 * 1024.0);

        return memoryUsageMB;
    }

    private double GetAvailableMemory()
    {
        var availablePhysicalMemory = new PerformanceCounter("Memory", "Available MBytes");
        return availablePhysicalMemory.NextValue();
    }

    private double GetCpuUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuCounter.NextValue();
        Thread.Sleep(1000);
        return cpuCounter.NextValue();
    }
}