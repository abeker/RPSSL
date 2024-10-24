using Microsoft.Extensions.DependencyInjection;
using RPSSL.Application.Choices.GetChoices;

namespace RPSSL.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetChoicesQuery).Assembly));
}