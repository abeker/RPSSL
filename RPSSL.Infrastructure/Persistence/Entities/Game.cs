using RPSSL.Domain.Games;

namespace RPSSL.Infrastructure.Persistence.Entities;

public class Game
{
    public Guid Id { get; init; }
    public Guid PlayerId { get; init; }
    public GameResult Result { get; init; }
}