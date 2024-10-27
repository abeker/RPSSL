using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Players;

public class Player : Entity
{
    public static readonly Player Computer = new(EntityId.Create(), PlayerName.Create("Computer").Value);
    public static readonly Player Anonymous = new(EntityId.Create(), PlayerName.Create("Anonymous").Value);
    
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