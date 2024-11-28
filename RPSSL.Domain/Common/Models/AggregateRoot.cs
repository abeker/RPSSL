﻿namespace RPSSL.Domain.Common.Models;

public abstract class AggregateRoot(EntityId id) : Entity(id)
{
    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    public void ClearDomainEvents() => domainEvents.Clear();
    protected void AddDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}