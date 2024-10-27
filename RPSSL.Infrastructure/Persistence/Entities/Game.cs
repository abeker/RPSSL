using RPSSL.Domain.Games;

namespace RPSSL.Infrastructure.Persistence.Entities;

public class Game
{
    public Guid Id { get; set; }
    public Player Player { get; set; }
    public GameResult Result { get; set; }
}