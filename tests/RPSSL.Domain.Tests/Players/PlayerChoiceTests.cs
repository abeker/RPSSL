using FluentAssertions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Players;
using Xunit;

namespace RPSSL.Domain.Tests.Players;

public class PlayerChoiceTests
{
    [Fact]
    public void Create_WhenCalledWithValidPlayerAndChoice_ThenReturnsSuccessResult()
    {
        // Arrange
        var playerResult = Player.Create(Guid.NewGuid(), PlayerName.Create("John Doe").Value);
        const Choice choice = Choice.Rock;

        // Act
        var result = PlayerChoice.Create(playerResult.Value, choice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Player.Should().Be(playerResult.Value);
        result.Value.Choice.Should().Be(choice);
    }

    [Fact]
    public void Create_WhenCalledWithNullPlayer_ThenThrowsArgumentException()
    {
        // Arrange
        Player? nullPlayer = null;
        const Choice choice = Choice.Rock;

        // Act
        Action act = () => PlayerChoice.Create(nullPlayer, choice);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_WhenCalledWithValidPlayerAndDefaultChoice_ThenReturnsSuccessResult()
    {
        // Arrange
        var playerResult = Player.Create(Guid.NewGuid(), PlayerName.Create("Jane Doe").Value).Value;
        const Choice choice = default;

        // Act
        var result = PlayerChoice.Create(playerResult, choice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Player.Should().Be(playerResult);
        result.Value.Choice.Should().Be(choice);
    }
}