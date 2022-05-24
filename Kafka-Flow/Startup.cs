using Kafka_Flow.ServiceCollectionExtensions.ConfigurationExtensions;
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


        services
            .AddMvc()
            .AddNewtonsoftJson();


        services.AddHttpContextManager();
        services.AddLogging(builder => builder.AddConsole());
        services.AddAutoMapper(typeof(Startup));
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env
    )
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoices API"); });

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}