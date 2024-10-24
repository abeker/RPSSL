using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Infrastructure.ApiClients.CodeChallenge;
using RPSSL.Infrastructure.Configuration;
using RPSSL.Infrastructure.Persistence;

namespace RPSSL.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidateOptions<CodeChallengeApiOptions>, CodeChallengeApiOptions>();
        services.AddOptions<CodeChallengeApiOptions>()
            .Bind(configuration.GetSection(CodeChallengeApiOptions.CodeChallengeApi))
            .ValidateOnStart();

        services
            .AddRefitClient<ICodeChallengeApiClient>()
            .ConfigureHttpClient(c =>
            {
                var apiOptions = configuration.GetSection(CodeChallengeApiOptions.CodeChallengeApi)
                    .Get<CodeChallengeApiOptions>();
                c.BaseAddress = new Uri(apiOptions!.BaseAddress);
            });
        
        services
            .AddScoped<IRandomNumberRepository, RandomNumberRepository>();

        return services;
    }
}