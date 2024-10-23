using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Player;

public class PlayerName : ValueObject
{
    public string Value { get; }

    private PlayerName(string value)
    {
        Value = value;
    }
    
    public static Result<PlayerName, ErrorList> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? new EmptyStringError(value).ToList() 
            : new PlayerName(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}