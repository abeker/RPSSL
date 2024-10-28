using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Common.Models;

public class EntityId : ValueObject
{
    public Guid Value { get; }
    
    private EntityId(Guid value)
    {
        Value = value;
    }

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