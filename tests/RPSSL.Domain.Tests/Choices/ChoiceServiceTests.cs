using FluentAssertions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Errors;
using Xunit;

namespace RPSSL.Domain.Tests.Choices;

public class ChoiceServiceTests
{
    private readonly ChoiceService choiceService = new();
    
    [Fact]
    public void GetByRandomNumber_WhenCalledWithValidPositiveNumber_ReturnsCorrectChoice()
    {
        // Arrange
        var positiveNumberResult = PositiveNumber.Create(1);

        // Act
        var result = choiceService.GetByRandomNumber(positiveNumberResult.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Choice.Rock);
    }
    
    [Fact]
    public void GetByRandomNumber_WhenCalledWithNullPositiveNumber_ReturnsFailureResult()
    {
        // Act
        var result = choiceService.GetByRandomNumber(null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().ContainSingle().Which.Should().BeOfType<NullValueError>();
    }
    
    [Fact]
    public void CalculateWinner_WhenChoicesAreEqual_ReturnsNone()
    {
        // Arrange
        const Choice choice = Choice.Rock;

        // Act
        var result = choiceService.CalculateWinner(choice, choice);

        // Assert
        result.HasNoValue.Should().BeTrue();
    }

    [Fact]
    public void CalculateWinner_WhenCalledWithInvalidChoice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const Choice invalidChoice = (Choice)999;

        // Act
        Action act = () => choiceService.CalculateWinner(invalidChoice, Choice.Rock);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(2, Choice.Paper)]
    [InlineData(3, Choice.Scissors)]
    [InlineData(4, Choice.Lizard)]
    [InlineData(5, Choice.Spock)]
    [InlineData(6, Choice.Rock)]
    [InlineData(11, Choice.Rock)]
    public void GetByRandomNumber_WhenCalledWithValidPositiveNumber_ReturnsExpectedChoice(int input, Choice expectedChoice)
    {
        // Arrange
        var positiveNumberResult = PositiveNumber.Create(input);

        // Act
        var result = choiceService.GetByRandomNumber(positiveNumberResult.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedChoice);
    }

    [Theory]
    [InlineData(Choice.Rock, Choice.Scissors)]
    [InlineData(Choice.Rock, Choice.Lizard)]
    [InlineData(Choice.Paper, Choice.Rock)]
    [InlineData(Choice.Paper, Choice.Spock)]
    [InlineData(Choice.Scissors, Choice.Paper)]
    [InlineData(Choice.Scissors, Choice.Lizard)]
    [InlineData(Choice.Lizard, Choice.Spock)]
    [InlineData(Choice.Lizard, Choice.Paper)]
    [InlineData(Choice.Spock, Choice.Scissors)]
    [InlineData(Choice.Spock, Choice.Rock)]
    public void CalculateWinner_WhenPlayerWins_ReturnsPlayerChoice(Choice playerChoice, Choice computerChoice)
    {
        // Act
        var result = choiceService.CalculateWinner(playerChoice, computerChoice);

        // Assert
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(playerChoice);
    }

    [Theory]
    [InlineData(Choice.Scissors, Choice.Rock)]
    [InlineData(Choice.Lizard, Choice.Rock)]
    [InlineData(Choice.Rock, Choice.Paper)]
    [InlineData(Choice.Spock, Choice.Paper)]
    [InlineData(Choice.Paper, Choice.Scissors)]
    [InlineData(Choice.Lizard, Choice.Scissors)]
    [InlineData(Choice.Spock, Choice.Lizard)]
    [InlineData(Choice.Paper, Choice.Lizard)]
    [InlineData(Choice.Scissors, Choice.Spock)]
    [InlineData(Choice.Rock, Choice.Spock)]
    public void CalculateWinner_WhenPlayerLoses_ReturnsComputerChoice(Choice playerChoice, Choice computerChoice)
    {
        // Act
        var result = choiceService.CalculateWinner(playerChoice, computerChoice);

        // Assert
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(computerChoice);
    }
}