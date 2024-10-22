using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Game;

public class Game : Entity
{
    public Player.Player Player { get; private set; }
    public Player.Player Computer { get; private set; }
    public Choice.Choice PlayerChoice { get; private set; }
    public Choice.Choice ComputerChoice { get; private set; }
    public GameResult Result { get; private set; }
    
    private Game(EntityId id, Player.Player player, Player.Player computer) : base(id)
    {
        Player = player;
        Computer = computer;
    }
    
    public static Result<Game, ErrorList> Create(EntityId id, Player.Player player, Player.Player computer)
    {
        return new Game(id, player, computer);
    }

    public void PlayRound(Choice.Choice playerChoice, Choice.Choice computerChoice)
    {
        PlayerChoice = playerChoice;
        ComputerChoice = computerChoice;
        Result = CalculateResult();
    }
    
    private GameResult CalculateResult()
    {
        // TODO: Logic to determine the winner
        return GameResult.WIN;
    }
}