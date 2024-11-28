using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Games.Persistence;
using RPSSL.Infrastructure.Persistence.Configuration;
using Game = RPSSL.Domain.Games.Game;

namespace RPSSL.Infrastructure.Persistence;

public class GameRepository(InMemoryDbContext context) : IGameRepository
{
    public async Task<Result<Game, ErrorList>> CreateAsync(Game game, CancellationToken cancellationToken)
    {
        await context.Games.AddAsync(game, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<Game, ErrorList>(game);
    }
}