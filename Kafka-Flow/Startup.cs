using Kafka_Flow.Kafka.Consumers.PrintMessage;
using Kafka_Flow.ServiceCollectionExtensions.ConfigurationExtensions;
using KafkaFlow;
using Lamar;

namespace Kafka_Flow;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureContainer(ServiceRegistry services)
    {
        services
            .Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });

        services.AddSwagger(Configuration);
        services.AddKafka(kafka =>
            kafka
                .UseConsoleLog()
                .AddCluster(cluster => cluster
                    .WithBrokers(new[] { "broker:29092" })
                    .AddConsumer(
                        consumer =>
                            consumer.Topic("purchases")
                                .WithGroupId("test-group")
                                .WithName("Test")
                                .WithInitialState(ConsumerInitialState.Running)
                                .WithBufferSize(20)
                                .WithWorkersCount(2)
                                .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                                .AddMiddlewares(m =>
                                    m.Add<PrintMessageMiddleware>())
                    )
                )
        );

        services
            .AddMvc()
            .AddNewtonsoftJson();


        services.AddHttpContextManager();
        services.AddLogging(builder => builder.AddConsole());
        services.AddAutoMapper(typeof(Startup));
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IHostApplicationLifetime lifetime
    )
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoices API"); });

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        lifetime.ApplicationStarted.Register(() =>
            app.ApplicationServices.CreateKafkaBus().StartAsync(lifetime.ApplicationStopped));
    }
}