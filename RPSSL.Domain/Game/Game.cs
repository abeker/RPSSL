using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Player;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Game;

public class Game : Entity
{
    public PlayerChoice PlayerChoice { get; }
    public PlayerChoice ComputerChoice { get; }
    public GameResult Result { get; private set; }
    
    private Game(EntityId id, PlayerChoice playerChoice, PlayerChoice computerChoice) : base(id)
    {
        PlayerChoice = playerChoice;
        ComputerChoice = computerChoice;
    }
    
    public static Result<Game, ErrorList> Create(EntityId id, PlayerChoice choice, PlayerChoice computerChoice)
    {
        return new Game(id, choice, computerChoice);
    }

    public void PlayRound(Choice.Choice choice, Choice.Choice computerChoice)
    {
        // TODO: Logic to determine the winner
        Result = GameResult.WIN;;
    }
}