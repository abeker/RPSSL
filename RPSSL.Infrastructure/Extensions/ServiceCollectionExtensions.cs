using Microsoft.Extensions.DependencyInjection;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Choices.Persistence;
using RPSSL.Infrastructure.Persistence;

namespace RPSSL.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddScoped<IRandomNumberRepository, RandomNumberRepository>()
            .AddScoped<IChoiceRepository, ChoiceRepository>();
}