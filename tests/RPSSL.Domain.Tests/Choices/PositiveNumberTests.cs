using FluentAssertions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Common.Errors;
using Xunit;

namespace RPSSL.Domain.Tests.Choices;

public class PositiveNumberTests
{
    [Fact]
    public void Create_WhenCalledWithPositiveValue_ThenReturnsSuccessResult()
    {
        // Arrange
        const int validValue = 5;

        // Act
        var result = PositiveNumber.Create(validValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validValue);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_WhenCalledWithInvalidValue_ThenReturnsFailureResult(int invalidValue)
    {
        // Act
        var result = PositiveNumber.Create(invalidValue);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<PositiveNumberOutOfRangeError>();
    }

    [Fact]
    public void Create_WhenCalledWithValidLargeValue_ThenReturnsSuccessResult()
    {
        // Arrange
        const int validLargeValue = 1_000_000;

        // Act
        var result = PositiveNumber.Create(validLargeValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validLargeValue);
    }
}