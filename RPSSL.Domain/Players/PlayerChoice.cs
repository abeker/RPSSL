using CSharpFunctionalExtensions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Guards;

namespace RPSSL.Domain.Players;

public class PlayerChoice : ValueObject
{
    public Player Player { get; }
    public Choice Choice { get; }

    private PlayerChoice(Player player, Choice choice)
    {
        Player = player;
        Choice = choice;
    }

    public static Result<PlayerChoice, ErrorList> Create(Player player, Choice choice)
    {
        Ensure.NotNull(player, nameof(player));
        
        return new PlayerChoice(player, choice);
    }
    
    protected override IEnumerable<string> GetEqualityComponents()
    {
        yield return Player.Id + Choice.ToString();
    }
}