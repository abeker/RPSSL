namespace RPSSL.Domain.Common.Models;

public abstract class Entity<T>
{
    public T Id { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(T id)
        : this()
    {
        Id = id;
    }
}