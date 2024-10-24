using RPSSL.Application.Extensions;
using RPSSL.Infrastructure.Extensions;
using Serilog;

namespace RPSSL.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddApi()
            .AddApplication()
            .AddInfrastructure();

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext());
    }
}