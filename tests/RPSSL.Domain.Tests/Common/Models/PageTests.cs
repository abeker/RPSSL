using FluentAssertions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using Xunit;

namespace RPSSL.Domain.Tests.Common.Models;

public class PageTests
{
    [Fact]
    public void Create_WhenIndexIsNegative_ThenReturnsFailureResult()
    {
        // Arrange
        const int negativeIndex = -1;
        const int size = 5;

        // Act
        var result = Page.Create(negativeIndex, size);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<PageOutOfRangeError>();
    }

    [Fact]
    public void Create_WhenSizeIsNegative_ThenReturnsFailureResult()
    {
        // Arrange
        const int index = 0;
        const int negativeSize = -5;

        // Act
        var result = Page.Create(index, negativeSize);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<PageOutOfRangeError>();
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    public void Create_WhenCalledWithValidIndexAndSize_ThenReturnsSuccessResult(int index, int size)
    {
        // Act
        var result = Page.Create(index, size);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Index.Should().Be(index);
        result.Value.Size.Should().Be(size);
    }
}