using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Application.Players.GetScoreboardQuery;

public class GetScoreboardQueryHandler(ILogger<GetScoreboardQueryHandler> logger, IPlayerRepository playerRepository)
    : IRequestHandler<GetScoreboardQuery, Result<ScoreboardResponse, ErrorList>>
{
    public async Task<Result<ScoreboardResponse, ErrorList>> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching scoreboard for page {PageIndex} - {PageSize}", request.Index, request.Size);
        return await Page.Create(request.Index, request.Size)
            .Bind(async page => await playerRepository.GetScoreboardByPageAsync(page, cancellationToken))
            .Map(players => new ScoreboardResponse(players.Select(player => player.Name.Value.ToString())));
    }
}