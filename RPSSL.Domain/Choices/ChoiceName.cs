using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices;

public class ChoiceName : ValueObject
{
    public string Value { get; }

    private ChoiceName(string value)
    {
        Value = value;
    }
    
    public static Result<ChoiceName, ErrorList> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? new EmptyStringError(value).ToList() 
            : new ChoiceName(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}