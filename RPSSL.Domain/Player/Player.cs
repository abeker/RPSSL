using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Models;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Player;

public class Player : Entity
{
    public string Name { get; }

    private Player(EntityId id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Player, ErrorList> Create(EntityId id, string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? new EmptyStringError(nameof(Name)).ToList()
            : new Player(id, name);
    }
}