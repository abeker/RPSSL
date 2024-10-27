using Microsoft.EntityFrameworkCore;
using RPSSL.Infrastructure.Persistence.Entities;

namespace RPSSL.Infrastructure.Persistence.Configuration;

public class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    
    public void Seed()
    {
        if (!Players.Any())
        {
            Players.AddRange(new Player
            {
                Id = Domain.Players.Player.Computer.Id.Value,
                Name = Domain.Players.Player.Computer.Name.Value
            },
            new Player
            {
                Id = Domain.Players.Player.Anonymous.Id.Value,
                Name = Domain.Players.Player.Anonymous.Name.Value
            });
            SaveChanges();
        }
    }
}