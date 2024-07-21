using Games.Domain.Games;
using General.Persistence.Stores;
using Microsoft.EntityFrameworkCore;

namespace Games.Persistence.Repositories {
    internal class GamesStore(DbContext context) : PaginableStore<Game>(context), IGamesStore;
}
