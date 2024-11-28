using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using RPSSL.Application.Common.Commands;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Application.Players.CreatePlayerCommand;

public class CreatePlayerCommandHandler(ILogger<CreatePlayerCommandHandler> logger, IPlayerRepository playerRepository) : ICommandHandler<CreatePlayerCommand, Result<PlayerResponse, ErrorList>>
{
    public async Task<Result<PlayerResponse, ErrorList>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a player '{PlayerName}'", request.Name);

        var playerName = PlayerName.Create(request.Name);
        
        return await playerName
            .Bind(async name => await playerRepository.GetByNameAsync(name, cancellationToken))
            .Ensure(p => p.HasNoValue, _ => new EntityAlreadyExistsError(nameof(Player)).ToList())
            .Bind(_ => Player.Create(Guid.NewGuid(), playerName.Value))
            .Bind(async player => await playerRepository.CreateAsync(player, cancellationToken))
            .Map(player => new PlayerResponse(player.Id, player.Name.Value))
            .Tap(() => logger.LogInformation("Player with name '{PlayerName}' successfully created", request.Name))
            .TapError(err => logger.LogError(err.ToString()));
    }
}