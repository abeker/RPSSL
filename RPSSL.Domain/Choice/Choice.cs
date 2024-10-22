using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;

namespace RPSSL.Domain.Choice;

public class Choice : Entity
{
    public ChoiceId Id { get; private set; }
    public string Name { get; private set; }
    
    private Choice(ChoiceId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<Choice, ErrorList> Create(ChoiceId id, string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? new EmptyStringError(nameof(Name)).ToList()
            : new Choice(id, name);
    }
}