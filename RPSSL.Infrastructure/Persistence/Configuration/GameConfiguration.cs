using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPSSL.Domain.Games;

namespace RPSSL.Infrastructure.Persistence.Configuration;

public sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(game => game.Id);
        
        builder.HasOne(game => game.Player)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(game => game.GameResult).IsRequired();
        
        builder.ToTable(nameof(Game));
    }
}