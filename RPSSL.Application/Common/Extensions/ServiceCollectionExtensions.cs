using Microsoft.Extensions.DependencyInjection;
using RPSSL.Application.Choices.GetChoices;
using RPSSL.Application.Choices.GetRandomChoice;
using RPSSL.Application.Games.PlayGameCommand;
using RPSSL.Application.Players.GetScoreboardQuery;
using RPSSL.Domain.Choices.Services;

namespace RPSSL.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetChoicesQuery).Assembly))
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetRandomChoiceQuery).Assembly))
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(PlayGameCommand).Assembly))
            .AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(GetScoreboardQuery).Assembly))
            .AddScoped<IChoiceService, ChoiceService>();
}