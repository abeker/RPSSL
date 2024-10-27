using RPSSL.Application.Common.Extensions;
using RPSSL.Infrastructure.Common.Extensions;

namespace RPSSL.Api.Common.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddApi()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);
    }
}