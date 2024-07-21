using Microsoft.EntityFrameworkCore.Design;

namespace Games.Persistence {
    internal class GamesContextFactory : IDesignTimeDbContextFactory<GamesContext> {
        public GamesContext CreateDbContext(string[] args) => new(null!, null!);
    }
}
