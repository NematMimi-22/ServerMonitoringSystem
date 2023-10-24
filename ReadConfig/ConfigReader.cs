using Microsoft.Extensions.Configuration;
namespace Weather_Monitoring.ReadConfig
{
    public class ConfigReader
    {
        public static ServerStatisticsConfig ReadConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var ServerIdentifier = configuration["ServerStatisticsConfig:ServerIdentifier"];
            var SamplingIntervalSeconds = int.Parse(configuration["ServerStatisticsConfig:SamplingIntervalSeconds"]);

            return new ServerStatisticsConfig(SamplingIntervalSeconds, ServerIdentifier);
        }
    }
}