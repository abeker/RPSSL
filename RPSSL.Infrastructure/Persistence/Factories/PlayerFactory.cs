using RPSSL.Infrastructure.Persistence.Entities;

namespace RPSSL.Infrastructure.Persistence.Factories;

public class PlayerFactory
{
    public Player Create(Domain.Players.Player player) => new()
    {
        Id = player.Id.Value,
        Name = player.Name.Value
    };
}