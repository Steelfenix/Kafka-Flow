using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore;

namespace Kafka_Flow;

public class Program
{
    public static IConfiguration Configuration { get; set; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
        .AddEnvironmentVariables()
        .Build();
        
    public static async Task Main(string[] args)
    {
        try
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseLamar()
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseConfiguration(Configuration);
                builder.UseStartup<Startup>();
            })
            .ConfigureServices(services =>
            {
                services.AddControllers();
            });
}