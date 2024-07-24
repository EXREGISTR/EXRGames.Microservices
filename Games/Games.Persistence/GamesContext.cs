using General.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Games.Persistence {
    internal class GamesContext(
        ISender sender, 
        IConfiguration configuration) : EXRGamesDbContext(sender) {
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            var connectionString = configuration.GetConnectionString("Connection");
            options.UseMySql(connectionString, 
                new MySqlServerVersion(new Version(8, 0, 36)));
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
