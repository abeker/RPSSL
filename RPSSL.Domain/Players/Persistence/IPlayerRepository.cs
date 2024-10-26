using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Players.Persistence;

public interface IPlayerRepository
{
    Task<Result<Player, ErrorList>> GetByName(PlayerName name);
}