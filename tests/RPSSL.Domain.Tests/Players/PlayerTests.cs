﻿using FluentAssertions;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using Xunit;

namespace RPSSL.Domain.Tests.Players;

public class PlayerTests
{
    [Fact]
    public void Create_WhenCalledWithValidIdAndName_ThenReturnsSuccessResult()
    {
        // Arrange
        var playerId = EntityId.Create();
        var playerName = PlayerName.Create("Alice").Value;

        // Act
        var result = Player.Create(playerId, playerName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(playerId);
        result.Value.Name.Should().Be(playerName);
    }

    [Fact]
    public void Create_WhenCalledWithNullId_ThenThrowsArgumentException()
    {
        // Arrange
        var playerName = PlayerName.Create("Alice").Value;

        // Act
        Action act = () => Player.Create(null, playerName);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_WhenCalledWithNullName_ThenThrowsArgumentException()
    {
        // Arrange
        var playerId = EntityId.Create();

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
        computerPlayer.Id.Should().NotBeNull();
    }
}