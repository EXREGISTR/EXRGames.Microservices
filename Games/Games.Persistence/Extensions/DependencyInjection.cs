using Games.Domain.Games;
using Games.Domain.Tags;
using Games.Domain.UserGames;
using Games.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using General.Persistence;

namespace Games.Persistence.Extensions {
    public static class DependencyInjection {
        public static IServiceCollection AddPersistence(this IServiceCollection services) {
            services.AddDatabaseContext<GamesContext>();

            services.AddScoped<IGamesStore, GamesStore>();
            services.AddScoped<ITagsStore, TagsStore>();
            services.AddScoped<IUserGamesStore, UserGamesStore>();
            return services; 
        }
    }
}
