using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Players.Persistence;

public interface IPlayerRepository
{
    Task<UnitResult<ErrorList>> CreateAsync(Player player);
    Task<Result<Maybe<Player>, ErrorList>> GetByNameAsync(PlayerName name);
}