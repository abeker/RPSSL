using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;

namespace RPSSL.Domain.Players.Persistence;

public interface IPlayerRepository
{
    Task<Result<Player, ErrorList>> CreateAsync(Player player, CancellationToken cancellationToken);
    Task<Result<Maybe<Player>, ErrorList>> GetByNameAsync(PlayerName name, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Player>, ErrorList>> GetScoreboardByPageAsync(Page page, CancellationToken cancellationToken);
}