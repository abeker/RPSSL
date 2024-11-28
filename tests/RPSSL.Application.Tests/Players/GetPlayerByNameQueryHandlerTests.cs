using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Players.GetPlayerByNameQuery;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using Xunit;

namespace RPSSL.Application.Tests.Players;

public class GetPlayerByNameQueryHandlerTests
{
    private readonly Mock<IPlayerRepository> playerRepositoryMock;
    private readonly GetPlayerByNameQueryHandler handler;

    public GetPlayerByNameQueryHandlerTests()
    {
        playerRepositoryMock = new Mock<IPlayerRepository>();
        Mock<ILogger<GetPlayerByNameQueryHandler>> loggerMock = new();
        handler = new GetPlayerByNameQueryHandler(loggerMock.Object, playerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenPlayerExists_ReturnsPlayerResponse()
    {
        var request = new GetPlayerByNameQuery("Alice");
        var player = Player.Create(EntityId.Create(), PlayerName.Create("Alice").Value).Value;

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe.From(player));

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new PlayerResponse(player.Id, player.Name.Value));
    }

    [Fact]
    public async Task Handle_WhenPlayerDoesNotExist_ReturnsEntityNotFoundError()
    {
        var request = new GetPlayerByNameQuery("Alice");

        playerRepositoryMock
            .Setup(repo => repo.GetByNameAsync(It.IsAny<PlayerName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe<Player>.None);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().ContainSingle().Which.Should().BeOfType<EntityNotFoundError>();
    }

    [Fact]
    public async Task Handle_WhenPlayerNameIsInvalid_ReturnsFailureResult()
    {
        var request = new GetPlayerByNameQuery("");

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}