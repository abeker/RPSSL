using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;

namespace RPSSL.Infrastructure.Persistence;

public class PlayerRepository(InMemoryDbContext context) : IPlayerRepository
{
    public async Task<Result<Player, ErrorList>> CreateAsync(Player player, CancellationToken cancellationToken)
    {
        await context.Players.AddAsync(player, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return player;
    }

    public async Task<Result<Maybe<Player>, ErrorList>> GetByNameAsync(PlayerName name, CancellationToken cancellationToken)
    {
        var player = await context.Set<Player>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name.Value == name.Value, cancellationToken: cancellationToken);
        
        if (player is null)
            return Result.Success<Maybe<Player>, ErrorList>(Maybe.None);

        return Result.Success<Maybe<Player>, ErrorList>(Player.Create(player.Id, PlayerName.Create(player.Name).Value).Value);
    }

    public async Task<Result<IEnumerable<Player>, ErrorList>> GetScoreboardByPageAsync(Page page, CancellationToken cancellationToken)
    {
        var playersWithWins = await context.Games
            .Include(g => g.Player)
            .GroupBy(g => new {g.Player.Id, g.Player.Name.Value})
            .Select(g => new 
            {
                Player = g.Key,
                Wins = g.Count(game => game.GameResult == GameResult.Win),
                Losses = g.Count(game => game.GameResult == GameResult.Lose)
            })
            .OrderByDescending(x => x.Wins)
            .ThenBy(x => x.Losses)
            .Skip(page.Index * page.Size)
            .Take(page.Size)
            .Select(x => x.Player)
            .ToListAsync(cancellationToken);

        return playersWithWins
            .Select(p => Player.Create(p.Id, PlayerName.Create(p.Value).Value).Value)
            .ToList();
    }
}