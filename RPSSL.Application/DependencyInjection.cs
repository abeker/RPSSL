using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RPSSL.Domain.Choices.Services;

namespace RPSSL.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddScoped<IChoiceService, ChoiceService>();
}