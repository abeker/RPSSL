using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Choice;

public class Choice : Entity
{
    public ChoiceCode Code { get; }
    public ChoiceName Name { get; }

    private Choice(EntityId id, ChoiceCode code, ChoiceName name) : base(id)
    {
        Code = code;
        Name = name;
    }

    public static Result<Choice, ErrorList> Create(EntityId id, ChoiceCode code, ChoiceName name)
    {
        return new Choice(id, code, name);
    }
}