using Microsoft.Extensions.DependencyInjection;
using RPSSL.Application.Choices.GetChoices;
using RPSSL.Application.Choices.GetRandomChoice;
using RPSSL.Domain.Choices.Services;

namespace RPSSL.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetChoicesQuery).Assembly))
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetRandomChoiceQuery).Assembly))
            .AddScoped<IChoiceService, ChoiceService>();
}