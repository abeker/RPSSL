namespace RPSSL.Domain.Common.Models;

public abstract class AggregateRoot<T> : Entity<T>
{
    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{T}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected AggregateRoot()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{T}"/> class.
    /// </summary>
    /// <param name="id">The aggregate root identifier.</param>
    protected AggregateRoot(T id)
        : base(id)
    {
    }
    
    public void ClearDomainEvents() => domainEvents.Clear();
    protected void AddDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}