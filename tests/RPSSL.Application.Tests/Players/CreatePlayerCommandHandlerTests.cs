using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Players.CreatePlayerCommand;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using Xunit;

namespace RPSSL.Application.Tests.Players;

public class CreatePlayerCommandHandlerTests
{
    private readonly Mock<IPlayerRepository> playerRepositoryMock;
    private readonly CreatePlayerCommandHandler handler;

    public CreatePlayerCommandHandlerTests()
    {
        playerRepositoryMock = new Mock<IPlayerRepository>();
        Mock<ILogger<CreatePlayerCommandHandler>> loggerMock = new();
        handler = new CreatePlayerCommandHandler(loggerMock.Object, playerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenPlayerDoesNotExist_CreatesPlayerSuccessfully()
    {
        var request = new CreatePlayerCommand("Alice");
        var playerName = PlayerName.Create("Alice").Value;
        var player = Player.Create(Guid.NewGuid(), playerName).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe<Player>.None);

        playerRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(player);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new PlayerResponse(player.Id, player.Name.Value));
    }

    [Fact]
    public async Task Handle_WhenPlayerAlreadyExists_ReturnsEntityAlreadyExistsError()
    {
        var request = new CreatePlayerCommand("Alice");
        var player = Player.Create(Guid.NewGuid(), PlayerName.Create("Alice").Value).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe.From(player));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().ContainSingle().Which.Should().BeOfType<EntityAlreadyExistsError>();
    }

    [Fact]
    public async Task Handle_WhenPlayerNameIsInvalid_ReturnsFailureResult()
    {
        var request = new CreatePlayerCommand("");

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}