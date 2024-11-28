using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Models;

namespace RPSSL.Domain.Players;

public class Player : AggregateRoot
{
    public static readonly Player Computer = new(EntityId.Create(), PlayerName.Create(nameof(Computer)).Value);
    public static readonly Player Anonymous = new(EntityId.Create(), PlayerName.Create(nameof(Anonymous)).Value);
    
    public PlayerName Name { get; }

    private Player(EntityId id, PlayerName name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Player, ErrorList> Create(EntityId id, PlayerName name)
    {
        if (id == null)
            throw new ArgumentException("Player id must be provided");
        
        if (name == null)
            throw new ArgumentException("Player name must be provided");
        
        return new Player(id, name);
    }
}