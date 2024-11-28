using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Domain.Games.Persistence;

public interface IGameRepository
{
    Task<Result<Game, ErrorList>> CreateAsync(Game game, CancellationToken cancellationToken);
}