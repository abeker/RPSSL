using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Infrastructure.Persistence;

public class PlayerRepository : IPlayerRepository
{
    public Task<Result<Player, ErrorList>> GetByName(PlayerName name)
    {
        // TODO: implement player fetching
        return Task.FromResult(Player.Create(EntityId.Create(), PlayerName.Create("aca123").Value));
    }
}