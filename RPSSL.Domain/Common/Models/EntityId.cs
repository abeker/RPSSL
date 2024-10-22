using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;

namespace RPSSL.Domain.Common.Models;

public class EntityId : ValueObject
{
    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static Result<EntityId, ErrorList> Create(Guid value)
    {
        return value == Guid.Empty
            ? new EmptyGuidError(nameof(EntityId)).ToList()
            : Result.Success<EntityId, ErrorList>(new EntityId(value));
    }

    public static EntityId Create()
    {
        return new EntityId(Guid.NewGuid());
    }

    protected override IEnumerable<string> GetEqualityComponents()
    {
        yield return Value.ToString();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}