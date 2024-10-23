using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Game;

public class Game : Entity
{
    public Choice.Choice PlayerChoice { get; }
    public Choice.Choice ComputerChoice { get; }
    public GameResult Result { get; private set; }
    
    private Game(EntityId id, Choice.Choice playerChoice, Choice.Choice computerChoice) : base(id)
    {
        PlayerChoice = playerChoice;
        ComputerChoice = computerChoice;
    }
    
    public static Result<Game, ErrorList> Create(EntityId id, Choice.Choice choice, Choice.Choice computerChoice)
    {
        return new Game(id, choice, computerChoice);
    }

    public void PlayRound(Choice.Choice choice, Choice.Choice computerChoice)
    {
        // TODO: Logic to determine the winner
        Result = GameResult.WIN;;
    }
}