using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;

namespace RPSSL.Domain.Players;

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
    
    public static implicit operator string (PlayerName name)
    {
        return name.Value;
    }
}