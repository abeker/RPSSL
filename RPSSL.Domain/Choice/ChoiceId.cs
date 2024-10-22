using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;

namespace RPSSL.Domain.Choice;

public class ChoiceId : ValueObject
{
    public int Value { get; private set; }

    private ChoiceId(int value)
    {
        Value = value;
    }

    public static Result<ChoiceId, ErrorList> Create(int value)
    {
        return value is < 1 or > 5
            ? new InvalidChoiceIdError(value).ToList() 
            : new ChoiceId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}