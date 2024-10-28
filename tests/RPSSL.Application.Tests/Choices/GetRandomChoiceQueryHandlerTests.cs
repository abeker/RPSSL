using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Choices.GetRandomChoice;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;
using Xunit;

namespace RPSSL.Application.Tests.Choices;

public class GetRandomChoiceQueryHandlerTests
{
    private readonly Mock<IRandomNumberRepository> randomNumberRepositoryMock;
    private readonly Mock<IChoiceService> choiceServiceMock;
    private readonly GetRandomChoiceQueryHandler handler;

    public GetRandomChoiceQueryHandlerTests()
    {
        randomNumberRepositoryMock = new Mock<IRandomNumberRepository>();
        choiceServiceMock = new Mock<IChoiceService>();
        handler = new GetRandomChoiceQueryHandler(
            randomNumberRepositoryMock.Object, 
            choiceServiceMock.Object, 
            new Mock<ILogger<GetRandomChoiceQueryHandler>>().Object);
    }

    [Fact]
    public async Task Handle_WhenCalled_ReturnsRandomChoice()
    {
        // Arrange
        var request = new GetRandomChoiceQuery();
        const int randomNumber = 1;
        const Choice choice = Choice.Rock;
        var expectedResponse = new RandomChoiceResponse((int)choice, choice.ToString());

        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(randomNumber);

        choiceServiceMock
            .Setup(service => service.GetByRandomNumber(It.IsAny<PositiveNumber>()))
            .Returns(choice);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Handle_WhenRandomNumberGenerationFails_ReturnsFailureResult()
    {
        // Arrange
        var request = new GetRandomChoiceQuery();
        var error = new ExternalApiError(nameof(ExternalApiError)).ToList();
        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int, ErrorList>(error));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenChoiceServiceReturnsFailure_ReturnsFailureResult()
    {
        // Arrange
        var request = new GetRandomChoiceQuery();
        const int randomNumber = 1;
        var error = new ExternalApiError(nameof(ExternalApiError)).ToList();

        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(randomNumber);

        choiceServiceMock
            .Setup(service => service.GetByRandomNumber(It.IsAny<PositiveNumber>()))
            .Returns(Result.Failure<Choice, ErrorList>(error));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
    }
}