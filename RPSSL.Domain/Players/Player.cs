using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Players;

public class Player : Entity
{
    public static readonly Player Computer = new(EntityId.Create(Guid.Parse("3F39C3E7-C8CE-40E5-841F-D606273D37A2")).Value, PlayerName.Create("Computer").Value);
    
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