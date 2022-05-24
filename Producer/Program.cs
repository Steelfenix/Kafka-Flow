using System.Net;
using Confluent.Kafka;
using Example.Common;
using Example.Common.Json;
using Newtonsoft.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    ClientId = $"{Guid.NewGuid()}-Dns.GetHostName()",
    Partitioner = Partitioner.Consistent
};

const string topic = "purchases";

string[] users = { "eabara", "jsmith", "sgarcia", "jbernard", "htanaka", "awalther" };
string[] items = { "book", "alarm clock", "t-shirts", "gift card", "batteries" };

using var producer = new ProducerBuilder<Null, string>(config).Build();

var numProduced = 0;
const int numMessages = 10;

for (var i = 0; i < numMessages; ++i)
{
    var rnd = new Random();
    var user = users[rnd.Next(users.Length)];
    var item = items[rnd.Next(items.Length)];

    var id = Guid.NewGuid();
    var order = new OrderDetail
    {
        Client = user,
        Item = item,
        Quantity = rnd.Next()
    };
    Console.WriteLine($"{id} - {order.AsJson()}");
    
    await producer.ProduceAsync(
        topic, 
        new Message<Null, string> { Value = order.AsJson()}
    );
}

producer.Flush(TimeSpan.FromSeconds(10));
Console.WriteLine($"{numProduced} messages were produced to topic {topic}");