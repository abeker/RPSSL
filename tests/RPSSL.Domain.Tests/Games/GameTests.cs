using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;
using Xunit;

namespace RPSSL.Domain.Tests.Games;

public class GameTests
{
    private readonly Mock<IChoiceService> choiceServiceMock = new();

    [Fact]
    public void Create_WhenCalledWithValidChoices_ThenReturnsSuccessResult()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var playerChoice = PlayerChoice.Create(Player.Create(gameId, PlayerName.Create("Alice").Value).Value, Choice.Rock).Value;
        var computerChoice = PlayerChoice.Create(Player.Computer, Choice.Scissors).Value;

        // Act
        var result = Game.Create(gameId, playerChoice.Player, playerChoice.Choice, computerChoice.Player, computerChoice.Choice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.PlayerChoice.Should().Be(playerChoice.Choice);
        result.Value.ComputerChoice.Should().Be(computerChoice.Choice);
    }

    [Fact]
    public void PlayRound_WhenCalledWithWinScenario_ThenUpdatesGameResultToWin()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var playerChoice = PlayerChoice.Create(Player.Create(gameId, PlayerName.Create("Alice").Value).Value, Choice.Rock).Value;
        var computerChoice = PlayerChoice.Create(Player.Computer, Choice.Scissors).Value;
        var game = Game.Create(gameId, playerChoice.Player, playerChoice.Choice, computerChoice.Player, computerChoice.Choice).Value;

        choiceServiceMock.Setup(c => c.CalculateWinner(playerChoice.Choice, computerChoice.Choice)).Returns(playerChoice.Choice);
        
        // Act
        game.PlayRound(choiceServiceMock.Object);

        // Assert
        game.GameResult.Should().Be(GameResult.Win);
    }

    [Fact]
    public void PlayRound_WhenCalledWithLoseScenario_ThenUpdatesGameResultToLose()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var playerChoice = PlayerChoice.Create(Player.Create(gameId, PlayerName.Create("Alice").Value).Value, Choice.Rock).Value;
        var computerChoice = PlayerChoice.Create(Player.Computer, Choice.Paper).Value;

        var game = Game.Create(gameId, playerChoice.Player, playerChoice.Choice, computerChoice.Player, computerChoice.Choice).Value;

        choiceServiceMock.Setup(c => c.CalculateWinner(playerChoice.Choice, computerChoice.Choice)).Returns(computerChoice.Choice);

        // Act
        game.PlayRound(choiceServiceMock.Object);

        // Assert
        game.GameResult.Should().Be(GameResult.Lose);
    }

    [Fact]
    public void PlayRound_WhenCalledWithTieScenario_ThenUpdatesGameResultToTie()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var playerChoice = PlayerChoice.Create(Player.Create(gameId, PlayerName.Create("Alice").Value).Value, Choice.Rock).Value;
        var computerChoice = PlayerChoice.Create(Player.Computer, Choice.Rock).Value;

        var game = Game.Create(gameId, playerChoice.Player, playerChoice.Choice, computerChoice.Player, computerChoice.Choice).Value;

        choiceServiceMock.Setup(c => c.CalculateWinner(playerChoice.Choice, computerChoice.Choice)).Returns(Maybe.None);

        // Act
        game.PlayRound(choiceServiceMock.Object);

        // Assert
        game.GameResult.Should().Be(GameResult.Tie);
    }
}