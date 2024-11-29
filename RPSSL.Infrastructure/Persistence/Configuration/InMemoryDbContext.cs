using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;

namespace RPSSL.Infrastructure.Persistence.Configuration;

public class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options, IMediator mediator) : DbContext(options)
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
    
    /// <summary>
    /// Saves all the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Publishes and then clears all the domain events that exist within the current transaction.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var aggregateRoots = ChangeTracker
            .Entries<AggregateRoot<Guid>>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();
        
        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());
        
        var tasks = domainEvents.Select(domainEvent => mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }
}