using Microsoft.Extensions.Logging;
using RPSSL.Application.Common.DomainEvents;

namespace RPSSL.Application.Players.CreatePlayer;

public class GreetNewPlayerOnPlayerCreatedDomainEventHandler(ILogger<GreetNewPlayerOnPlayerCreatedDomainEventHandler> logger) 
    : IDomainEventHandler<Domain.Players.DomainEvents.PlayerCreatedDomainEvent>
{
    public async Task Handle(Domain.Players.DomainEvents.PlayerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("New player created: '{PlayerId}'. Welcome to the system!", notification.Id);

        await Task.CompletedTask;
    }
}