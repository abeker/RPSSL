using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;
using RPSSL.Infrastructure.Persistence.Factories;

namespace RPSSL.Infrastructure.Persistence;

public class PlayerRepository(InMemoryDbContext context, PlayerFactory playerFactory) : IPlayerRepository
{
    public async Task<Result<Player, ErrorList>> CreateAsync(Player player, CancellationToken cancellationToken)
    {
        await context.Players.AddAsync(playerFactory.Create(player), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return player;
    }

    public async Task<Result<Maybe<Player>, ErrorList>> GetByNameAsync(PlayerName name, CancellationToken cancellationToken)
    {
        var player = await context.Players
            .FirstOrDefaultAsync(p => p.Name == name.Value, cancellationToken: cancellationToken);
        
        if (player is null)
            return Result.Success<Maybe<Player>, ErrorList>(Maybe.None);

        return Result.Success<Maybe<Player>, ErrorList>(Player.Create(EntityId.Create(player.Id).Value, PlayerName.Create(player.Name).Value).Value);
    }

    public async Task<Result<IEnumerable<Player>, ErrorList>> GetScoreboardByPageAsync(Page page, CancellationToken cancellationToken)
    {
        var playersWithWins = await context.Games
            .Include(g => g.Player)
            .GroupBy(g => g.Player)
            .Select(g => new 
            {
                Player = g.Key,
                Wins = g.Count(game => game.Result == GameResult.Win),
                Losses = g.Count(game => game.Result == GameResult.Lose)
            })
            .OrderByDescending(x => x.Wins)
            .ThenBy(x => x.Losses)
            .Skip(page.Index * page.Size)
            .Take(page.Size)
            .Select(x => x.Player)
            .ToListAsync(cancellationToken);

        return playersWithWins
            .Select(p => Player.Create(EntityId.Create(p.Id).Value, PlayerName.Create(p.Name).Value).Value)
            .ToList();
    }
}