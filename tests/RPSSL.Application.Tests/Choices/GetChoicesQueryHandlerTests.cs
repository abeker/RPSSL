using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Choices.GetChoices;
using RPSSL.Domain.Choices;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace RPSSL.Application.Tests.Choices;

public class GetChoicesQueryHandlerTests
{
    private readonly GetChoicesQueryHandler handler;

    public GetChoicesQueryHandlerTests()
    {
        Mock<ILogger<GetChoicesQueryHandler>> loggerMock = new();
        handler = new GetChoicesQueryHandler(loggerMock.Object);
    }
    
    [Fact]
    public async Task Handle_WhenCalled_ReturnsAllChoices()
    {
        // Arrange
        var request = new GetChoicesQuery();
        var expectedChoices = Enum.GetValues(typeof(Choice))
            .Cast<Choice>()
            .Select(choice => new ChoiceResponse((int)choice, choice.ToString()))
            .ToList();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedChoices);
    }
}