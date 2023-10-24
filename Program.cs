// See https://aka.ms/new-console-template for more information
using Weather_Monitoring.ReadConfig;

var t=ConfigReader.ReadConfig();
Console.WriteLine(t.serverIdentifier);
Console.WriteLine(t.samplingIntervalSeconds);