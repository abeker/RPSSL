using FluentAssertions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using Xunit;

namespace RPSSL.Domain.Tests.Common.Models;

public class EntityIdTests
{
    [Fact]
    public void Create_WhenCalledWithEmptyGuid_ThenReturnsFailureResult()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        var result = EntityId.Create(emptyGuid);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Single().Should().BeOfType<EmptyGuidError>();
    }

    [Theory]
    [InlineData("f47ac10b-58cc-4372-a567-0e02b2c3d479")]
    [InlineData("d9b9ee5a-d587-442b-8995-84b77f5c2959")]
    public void Create_WhenCalledWithValidGuid_ThenReturnsSuccessResult(string validGuidString)
    {
        // Arrange
        var validGuid = Guid.Parse(validGuidString);

        // Act
        var result = EntityId.Create(validGuid);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validGuid);
    }

    [Fact]
    public void Create_WhenCalledWithoutParameters_ThenReturnsNewEntityId()
    {
        // Act
        var result = EntityId.Create();

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBe(Guid.Empty);
    }
}