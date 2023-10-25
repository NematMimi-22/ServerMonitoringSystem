using ServerMonitoringSystem.RabbitMQMessage;
var messageQueue = new RabbitMQMessageQueue("localhost", "test");
var statisticsCollector = new ServerStatisticsCollector(messageQueue);
statisticsCollector.StartCollecting();
Console.WriteLine("Server Statistics Collection Service is running.");