using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using RPSSL.Application.Common.Commands;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Application.Players;

public class CreatePlayerCommandHandler(ILogger<CreatePlayerCommandHandler> logger, IPlayerRepository playerRepository) : ICommandHandler<CreatePlayerCommand, UnitResult<ErrorList>>
{
    public async Task<UnitResult<ErrorList>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a player '{PlayerName}'", request.Name);

        var playerName = PlayerName.Create(request.Name);
        
        return await playerName
            .Bind(async name => await playerRepository.GetByNameAsync(name))
            .Ensure(p => p.HasNoValue, _ => new EntityAlreadyExistsError(nameof(Player)).ToList())
            .Bind(_ => Player.Create(EntityId.Create(), playerName.Value))
            .Bind(async player => await playerRepository.CreateAsync(player))
            .Tap(() => logger.LogInformation("Player with name '{PlayerName}' successfully created", request.Name))
            .TapError(err => logger.LogError(err.ToString()));
    }
}