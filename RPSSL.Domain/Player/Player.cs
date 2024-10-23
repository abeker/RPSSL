using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Player;

public class Player : Entity
{
    public PlayerName Name { get; }

    private Player(EntityId id, PlayerName name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Player, ErrorList> Create(EntityId id, PlayerName name)
    {
        return new Player(id, name);
    }
}