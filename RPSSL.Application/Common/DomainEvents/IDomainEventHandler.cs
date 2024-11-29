using MediatR;
using RPSSL.Domain.Common.Models;

namespace RPSSL.Application.Common.DomainEvents;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}