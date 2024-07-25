using Games.Domain;
using Games.Domain.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Games.Persistence.Configurations {
    internal class GameConfiguration : IEntityTypeConfiguration<Game> {
        public void Configure(EntityTypeBuilder<Game> builder) {
            builder.ToTable("games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(Game.MaxTitleLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired();

            builder.OwnsOne(x => x.Price, propertyBuilder => {
                propertyBuilder
                    .Property(price => price.Value)
                    .HasColumnName("Price")
                    .IsRequired();
            });

            builder.HasMany(x => x.Tags)
                .WithMany()
                .UsingEntity<GameTag>(x => {
                    x.ToTable("game_tags");
                    x.Property(x => x.GameId)
                        .HasColumnName("GameId");
                    x.Property(x => x.TagId)
                        .HasColumnName("TagId");
                });
        }
    }
}
