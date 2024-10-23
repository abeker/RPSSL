using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RPSSL.Api.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RPSSL.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services.AddApiVersioning(
            config => {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

        services.AddRouting(options => options.LowercaseUrls = true);
        
        services.AddSwaggerGen(c => {
            c.UseOneOfForPolymorphism();
            c.SwaggerDoc("v1.0", new OpenApiInfo {
                Title = "Search Proxy API",
                Version = "v1.0"
            });
            c.DocInclusionPredicate((docName, apiDesc) =>
                apiDesc.TryGetMethodInfo(out var methodInfo)
                && (methodInfo.DeclaringType
                        ?.GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .Any(v => $"v{v}" == docName)
                    ?? false));
            c.OperationFilter<RemoveVersionParameterFilter>();
            c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddCors(options => {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
        });

        return services;
    }
}