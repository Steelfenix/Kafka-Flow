using Microsoft.OpenApi.Models;

namespace Kafka_Flow.ServiceCollectionExtensions.ConfigurationExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KafkaFlow API",
                    Description = $"<h4>Environment: {configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT")}</h4>",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }
    }
}