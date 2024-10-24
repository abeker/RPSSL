using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices;

public class PositiveNumber : ValueObject
{
    public int Value { get; }

    private PositiveNumber(int value)
    {
        Value = value;
    }
    
    public static Result<PositiveNumber, ErrorList> Create(int value)
    {
        return value < 1
            ? new PositiveNumberOutOfRangeError(value).ToList() 
            : new PositiveNumber(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}