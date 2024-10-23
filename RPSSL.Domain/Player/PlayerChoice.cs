using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Player;

public class PlayerChoice : ValueObject
{
    public Player Player { get; }
    public Choice.Choice Choice { get; }

    private PlayerChoice(Player player, Choice.Choice choice)
    {
        Player = player;
        Choice = choice;
    }

    public static Result<PlayerChoice, ErrorList> Create(Player player, Choice.Choice choice)
    {
        return new PlayerChoice(player, choice);
    }
    
    protected override IEnumerable<string> GetEqualityComponents()
    {
        yield return Player.Id + Choice.Id.ToString();
    }
}