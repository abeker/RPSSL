using System.ComponentModel.DataAnnotations;

namespace RPSSL.Infrastructure.Persistence.Entities;

public class Player
{
    public Guid Id { get; init; }
    
    [MaxLength(100)]
    public string Name { get; init; }
}