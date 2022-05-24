namespace Kafka_Flow.ServiceCollectionExtensions.ConfigurationExtensions
{
    public static class HttpContextManagerExtension
    {
        public static IServiceCollection AddHttpContextManager(this IServiceCollection service)
        {
            return service
                .AddHttpContextAccessor();
        }
    }
}