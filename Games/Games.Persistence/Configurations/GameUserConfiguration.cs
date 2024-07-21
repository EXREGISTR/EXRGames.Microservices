using Games.Domain.UserGames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Games.Persistence.Configurations {
    internal class GameUserConfiguration : IEntityTypeConfiguration<UserGame> {
        public void Configure(EntityTypeBuilder<UserGame> builder) {
            builder.ToTable("user_games");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.GameId)
                .HasColumnName("GameId")
                .IsRequired();
        }
    }
}
