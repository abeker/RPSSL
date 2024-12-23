﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Application.Players.GetPlayerByName;

public class GetPlayerByNameQueryHandler(ILogger<GetPlayerByNameQueryHandler> logger, IPlayerRepository playerRepository) : IRequestHandler<GetPlayerByNameQuery, Result<PlayerResponse, ErrorList>>
{
    public async Task<Result<PlayerResponse, ErrorList>> Handle(GetPlayerByNameQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching player with name '{PlayerName}'", request.Name);
        
        var playerName = PlayerName.Create(request.Name);
        return await playerName
            .Bind(async name => await playerRepository.GetByNameAsync(name, cancellationToken))
            .Ensure(p => p.HasValue, _ => new EntityNotFoundError(nameof(Player)).ToList())
            .Map(player => new PlayerResponse(player.Value.Id, player.Value.Name.Value))
            .Tap(() => logger.LogInformation("Player with name '{PlayerName}' successfully fetched", request.Name))
            .TapError(err => logger.LogError(err.ToString()));
    }
}