using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RPSSL.Application.Players.GetScoreboardQuery;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using Xunit;

namespace RPSSL.Application.Tests.Players;

public class GetScoreboardQueryHandlerTests
{
    private readonly Mock<IPlayerRepository> playerRepositoryMock;
    private readonly GetScoreboardQueryHandler handler;

    public GetScoreboardQueryHandlerTests()
    {
        playerRepositoryMock = new Mock<IPlayerRepository>();
        Mock<ILogger<GetScoreboardQueryHandler>> loggerMock = new();
        handler = new GetScoreboardQueryHandler(loggerMock.Object, playerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ReturnsScoreboardResponse()
    {
        var request = new GetScoreboardQuery(1, 10);
        var players = new List<Player>
        {
            Player.Create(Guid.NewGuid(), PlayerName.Create("Alice").Value).Value,
            Player.Create(Guid.NewGuid(), PlayerName.Create("Bob").Value).Value
        };

        playerRepositoryMock
            .Setup(repo => repo.GetScoreboardByPageAsync(It.IsAny<Page>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(players);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.PlayerNames.Should().BeEquivalentTo(["Alice", "Bob"]);
    }

    [Fact]
    public async Task Handle_WhenNoPlayersFound_ReturnsEmptyScoreboardResponse()
    {
        var request = new GetScoreboardQuery(1, 10);
        var players = new List<Player>();

        playerRepositoryMock
            .Setup(repo => repo.GetScoreboardByPageAsync(It.IsAny<Page>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(players);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.PlayerNames.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_WhenPageIsInvalid_ReturnsFailureResult()
    {
        var request = new GetScoreboardQuery(-1, 10);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}