using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Guards;
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
        Ensure.NotEmpty(id, $"{nameof(Player)} {nameof(Id)} must be provided", nameof(id));
        Ensure.NotNull(name, nameof(name));
        
        return new Player(id, name);
    }
}