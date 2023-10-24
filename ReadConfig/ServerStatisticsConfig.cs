public class ServerStatisticsConfig
{
    public int samplingIntervalSeconds { get; set; }
    public string serverIdentifier { get; set; }

    public ServerStatisticsConfig(int samplingIntervalSeconds, string serverIdentifier)
    {
        this.samplingIntervalSeconds = samplingIntervalSeconds;
        this.serverIdentifier = serverIdentifier;
    }
}