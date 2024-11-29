using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Games.Persistence;
using RPSSL.Domain.Players.Persistence;
using RPSSL.Infrastructure.ApiClients.CodeChallenge;
using RPSSL.Infrastructure.Configuration;
using RPSSL.Infrastructure.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;

namespace RPSSL.Infrastructure;

public static class DependencyInjection
{
    private const string DbName = "RpsslDb";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidateOptions<CodeChallengeApiOptions>, CodeChallengeApiOptions>();
        services.AddOptions<CodeChallengeApiOptions>()
            .Bind(configuration.GetSection(CodeChallengeApiOptions.CodeChallengeApi))
            .ValidateOnStart();
        
        services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase(DbName));

        services
            .AddRefitClient<ICodeChallengeApiClient>()
            .ConfigureHttpClient(c =>
            {
                var apiOptions = configuration.GetSection(CodeChallengeApiOptions.CodeChallengeApi)
                    .Get<CodeChallengeApiOptions>();
                c.BaseAddress = new Uri(apiOptions!.BaseAddress);
            });

        services
            .AddScoped<IRandomNumberRepository, RandomNumberRepository>()
            .AddScoped<IPlayerRepository, PlayerRepository>()
            .AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}