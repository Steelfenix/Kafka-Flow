using System.Net;
using Confluent.Kafka;
using Example.Common;
using Example.Common.Json;
using Newtonsoft.Json;

Console.WriteLine("Works!!!");

var config = new ConsumerConfig
{
    BootstrapServers = "broker:29092",
    GroupId = "test",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = true,
    PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
};
const string topic = "purchases";

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe(topic);

while (true)
{
    var result = consumer.Consume();
    try
    {
        var order = JsonConvert.DeserializeObject<OrderDetail>(result.Message.Value);
        Console.WriteLine($"{result.Message.Key} - {order.AsJson()} {result.Partition}");
    }
    catch (Exception e)
    {
        Console.WriteLine(result.Message.Value);
        Console.WriteLine(e.Message);
    }

    await Task.Delay(2000);
}