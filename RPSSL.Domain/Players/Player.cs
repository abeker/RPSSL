using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Models;

namespace RPSSL.Domain.Players;

public class Player : AggregateRoot<Guid>
{
    public static readonly Player Computer = new(Guid.NewGuid(), PlayerName.Create(nameof(Computer)).Value);
    public static readonly Player Anonymous = new(Guid.NewGuid(), PlayerName.Create(nameof(Anonymous)).Value);
    
    public PlayerName Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Player()
    {
    }

    private Player(Guid id, PlayerName name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Player, ErrorList> Create(Guid id, PlayerName name)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Player id must be provided");
        
        if (name == null)
            throw new ArgumentException("Player name must be provided");
        
        return new Player(id, name);
    }
}