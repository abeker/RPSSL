using CSharpFunctionalExtensions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;

namespace RPSSL.Domain.Games;

public class Game : AggregateRoot<Guid>
{
    public Player Player { get; }
    public Choice PlayerChoice { get; }
    public Player Computer { get; }
    public Choice ComputerChoice { get; }
    public GameResult GameResult { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Game()
    {
    }

    private Game(Guid id, Player player, Choice playerChoice, Player computer, Choice computerChoice) : base(id)
    {
        Player = player;
        PlayerChoice = playerChoice;
        Computer = computer;
        ComputerChoice = computerChoice;
    }
    
    public static Result<Game, ErrorList> Create(Guid id, Player player, Choice playerChoice, Player computer, Choice computerChoice)
    {
        return new Game(id, player, playerChoice, computer, computerChoice);
    }
    
    public Result<Game, ErrorList> PlayRound(IChoiceService choiceService)
    {
        var winner = choiceService.CalculateWinner(PlayerChoice, ComputerChoice);
        GameResult = winner.HasNoValue
            ? GameResult.Tie
            : winner.Value == PlayerChoice 
                ? GameResult.Win 
                : GameResult.Lose;
        
        return this;
    }
}