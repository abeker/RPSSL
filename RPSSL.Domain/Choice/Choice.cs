using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;

namespace RPSSL.Domain.Choice;

public class Choice : ValueObject
{
    public Player.Player Player { get; }
    public ChoiceOption ChoiceOption { get; }

    private Choice(Player.Player player, ChoiceOption choiceOption)
    {
        Player = player;
        ChoiceOption = choiceOption;
    }

    public static Result<Choice, ErrorList> Create(Player.Player player, ChoiceOption choiceOption)
    {
        return new Choice(player, choiceOption);
    }

    protected override IEnumerable<string> GetEqualityComponents()
    {
        yield return Player.Id.ToString() + ChoiceOption;
    }
}