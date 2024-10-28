using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Application.Games.PlayGameCommand;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Games;
using RPSSL.Domain.Games.Persistence;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using Xunit;

namespace RPSSL.Application.Tests.Games;

public class PlayGameCommandHandlerTests
{
    private readonly Mock<IPlayerRepository> playerRepositoryMock;
    private readonly Mock<IChoiceService> choiceServiceMock;
    private readonly Mock<IRandomNumberRepository> randomNumberRepositoryMock;
    private readonly PlayGameCommandHandler handler;

    public PlayGameCommandHandlerTests()
    {
        playerRepositoryMock = new Mock<IPlayerRepository>();
        choiceServiceMock = new Mock<IChoiceService>();
        randomNumberRepositoryMock = new Mock<IRandomNumberRepository>();
        Mock<IGameRepository> gameRepositoryMock = new();
        Mock<ILogger<PlayGameCommandHandler>> loggerMock = new();

        handler = new PlayGameCommandHandler(
            playerRepositoryMock.Object, 
            choiceServiceMock.Object, 
            randomNumberRepositoryMock.Object,
            gameRepositoryMock.Object,
            loggerMock.Object);
    }

    [Fact]
    public async Task Handle_WhenPlayerExistsAndChoicesAreValid_ReturnsGameResponse()
    {
        const Choice computerChoice = Choice.Scissors;
        const Choice playerChoice = Choice.Rock;
        var request = new PlayGameCommand("Alice", (int)playerChoice);
        var player = Player.Create(EntityId.Create(), PlayerName.Create("Alice").Value).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe.From(player));

        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        choiceServiceMock
            .Setup(service => service.GetByRandomNumber(It.IsAny<PositiveNumber>()))
            .Returns(computerChoice);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Results.Should().Be(GameResult.Tie.ToString().ToLowerInvariant());
        result.Value.Player.Should().Be((int)playerChoice);
        result.Value.Computer.Should().Be((int)computerChoice);
    }
    
    [Fact]
    public async Task Handle_WhenPlayerDoesNotExist_ReturnsEntityNotFoundError()
    {
        var request = new PlayGameCommand("Alice", (int)Choice.Rock);

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<Maybe<Player>, ErrorList>(new EntityNotFoundError(nameof(Player)).ToList()));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_WhenRandomNumberGenerationFails_ReturnsFailureResult()
    {
        var request = new PlayGameCommand("Alice", (int)Choice.Rock);
        var player = Player.Create(EntityId.Create(), PlayerName.Create("Alice").Value).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe.From(player));

        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int, ErrorList>(new ExternalApiError(nameof(ExternalApiError)).ToList()));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenChoiceServiceReturnsFailure_ReturnsFailureResult()
    {
        var request = new PlayGameCommand("Alice", (int)Choice.Rock);
        var player = Player.Create(EntityId.Create(), PlayerName.Create("Alice").Value).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe.From(player));

        randomNumberRepositoryMock
            .Setup(repo => repo.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        choiceServiceMock
            .Setup(service => service.GetByRandomNumber(It.IsAny<PositiveNumber>()))
            .Returns(Result.Failure<Choice, ErrorList>(new ExternalApiError(nameof(ExternalApiError)).ToList()));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenPlayerMakesInvalidChoice_ReturnsFailureResult()
    {
        var request = new PlayGameCommand("Alice", 99); // Invalid choice

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}