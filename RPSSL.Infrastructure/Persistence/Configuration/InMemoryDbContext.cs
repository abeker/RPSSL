using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;

namespace RPSSL.Infrastructure.Persistence.Configuration;

public class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; init; }
    public DbSet<Player> Players { get; init; }
    
    public void Seed()
    {
        if (!Players.Any())
        {
            Players.AddRange(Player.Computer, Player.Anonymous);
            SaveChanges();
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}