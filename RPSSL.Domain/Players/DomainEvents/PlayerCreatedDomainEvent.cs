using RPSSL.Domain.Common.Models;

namespace RPSSL.Domain.Players.DomainEvents;

public class PlayerCreatedDomainEvent(Guid id) : IDomainEvent
{
    public Guid Id { get; private set; } = id;
}