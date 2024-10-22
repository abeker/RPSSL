namespace RPSSL.Domain.Common.Models;

public abstract class Entity(EntityId id)
{
    public EntityId Id { get; } = id;
}