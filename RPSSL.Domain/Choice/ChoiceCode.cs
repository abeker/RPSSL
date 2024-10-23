using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choice;

public class ChoiceCode : ValueObject
{
    public int Value { get; }
    
    private ChoiceCode(int value)
    {
        Value = value;
    }
    
    public static Result<ChoiceCode, ErrorList> Create(int value)
    {
        return value < 1
            ? new InvalidChoiceIdError(value).ToList() 
            : new ChoiceCode(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}