using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPSSL.Domain.Players;

namespace RPSSL.Infrastructure.Persistence.Configuration;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(player => player.Id);
        
        builder.OwnsOne(player => player.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Player.Name))
                .HasMaxLength(PlayerName.NameMaxLength)
                .IsRequired();
        });
        
        builder.ToTable(nameof(Player));
    }
}