using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Games.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;
using Game = RPSSL.Domain.Games.Game;

namespace RPSSL.Infrastructure.Persistence;

public class GameRepository(InMemoryDbContext context) : IGameRepository
{
    public async Task<Result<Game, ErrorList>> CreateAsync(Game game, CancellationToken cancellationToken)
    {
        await context.Games.AddAsync(new Entities.Game
        {
            Id = game.Id.Value,
            PlayerId = game.PlayerChoice.Player.Id.Value,
            Result = game.GameResult
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<Game, ErrorList>(game);
    }
}