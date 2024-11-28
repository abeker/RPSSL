using FluentAssertions;
using RPSSL.Domain.Players;
using Xunit;

namespace RPSSL.Domain.Tests.Players;

public class PlayerTests
{
    [Fact]
    public void Create_WhenCalledWithValidIdAndName_ThenReturnsSuccessResult()
    {
        // Arrange
        var playerId = Guid.NewGuid();
        var playerName = PlayerName.Create("Alice").Value;

        // Act
        var result = Player.Create(playerId, playerName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(playerId);
        result.Value.Name.Should().Be(playerName);
    }

    [Fact]
    public void Create_WhenCalledWithNullName_ThenThrowsArgumentException()
    {
        // Arrange
        var playerId = Guid.NewGuid();

        // Act
        Action act = () => Player.Create(playerId, null);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Computer_ShouldHaveValidProperties()
    {
        // Arrange
        var computerPlayer = Player.Computer;

        // Act & Assert
        computerPlayer.Name.Value.Should().Be("Computer");
        computerPlayer.Id.Should().NotBeEmpty();
    }
}