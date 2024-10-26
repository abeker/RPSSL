using FluentAssertions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Players;
using Xunit;

namespace RPSSL.Domain.Tests.Players;

public class PlayerNameTests
{
    [Fact]
    public void Create_WhenCalledWithEmptyString_ThenReturnsFailureResult()
    {
        // Arrange
        var emptyName = string.Empty;

        // Act
        var result = PlayerName.Create(emptyName);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<EmptyStringError>();
    }

    [Fact]
    public void Create_WhenCalledWithWhitespace_ThenReturnsFailureResult()
    {
        // Arrange
        const string whitespaceName = "    ";

        // Act
        var result = PlayerName.Create(whitespaceName);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<EmptyStringError>();
    }

    [Fact]
    public void Create_WhenCalledWithNull_ThenReturnsFailureResult()
    {
        // Arrange
        string? nullName = null;

        // Act
        var result = PlayerName.Create(nullName);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<EmptyStringError>();
    }

    [Theory]
    [InlineData("A")]
    [InlineData("Player")]
    [InlineData("John Doe")]
    public void Create_WhenCalledWithVariousValidNames_ThenReturnsSuccessResult(string validName)
    {
        // Act
        var result = PlayerName.Create(validName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validName);
    }
}