using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;

namespace RPSSL.Infrastructure.Persistence;

public class PlayerRepository(InMemoryDbContext context) : IPlayerRepository
{
    public async Task<Result<Player, ErrorList>> GetByName(PlayerName name)
    {
        var player = await context.Players
            .FirstOrDefaultAsync(p => p.Name == name.Value);
        
        if (player is null)
            return Result.Failure<Player, ErrorList>(new EntityNotFoundError(nameof(Entities.Player)).ToList());

        return Player.Create(EntityId.Create(player.Id).Value, PlayerName.Create(player.Name).Value);
    }
}