using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;

namespace RPSSL.Infrastructure.Persistence;

public class PlayerRepository(InMemoryDbContext context) : IPlayerRepository
{
    public async Task<UnitResult<ErrorList>> CreateAsync(Player player)
    {
        await context.Players.AddAsync(new Entities.Player
        {
            Id = player.Id.Value,
            Name = player.Name.Value
        });

        await context.SaveChangesAsync();

        return UnitResult.Success<ErrorList>();
    }

    public async Task<Result<Maybe<Player>, ErrorList>> GetByNameAsync(PlayerName name)
    {
        var player = await context.Players
            .FirstOrDefaultAsync(p => p.Name == name.Value);
        
        if (player is null)
            return Result.Success<Maybe<Player>, ErrorList>(Maybe.None);

        return Result.Success<Maybe<Player>, ErrorList>(Player.Create(EntityId.Create(player.Id).Value, PlayerName.Create(player.Name).Value).Value);
    }
}