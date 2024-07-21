using Games.Domain.UserGames;
using General.Persistence.Stores;
using Microsoft.EntityFrameworkCore;

namespace Games.Persistence.Repositories {
    internal class UserGamesStore(DbContext context) : PaginableStore<UserGame>(context), IUserGamesStore;
}
