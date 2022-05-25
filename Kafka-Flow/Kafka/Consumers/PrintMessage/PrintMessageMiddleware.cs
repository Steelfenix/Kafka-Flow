using Kafka_Flow.ServiceCollectionExtensions.GenericsExtension;
using KafkaFlow;
using Newtonsoft.Json;

namespace Kafka_Flow.Kafka.Consumers.PrintMessage;

public class PrintMessageMiddleware: IMessageMiddleware
{
    private readonly ILogger<PrintMessageMiddleware> _logger;

    public PrintMessageMiddleware(
        ILogger<PrintMessageMiddleware> logger
    )
    {
        _logger = logger;
    }
    
    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var value = context.Message.Value.GetStringFromByteArray();
        var key = context.Message.Key.GetStringFromByteArray();
        
        _logger.LogInformation("{Key} - {Value} - {Partition}",
            key, 
            value,
            context.ConsumerContext.Partition);
        
        return Task.CompletedTask;
    }
}