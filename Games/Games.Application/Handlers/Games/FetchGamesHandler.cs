using Games.Contracts.Requests.Games;
using Games.Domain.Game;
using General.Contracts;
using General.Domain;
using General.Domain.Extensions;
using General.Domain.Results;

namespace Games.Application.Handlers.Games {
    internal class FetchGamesHandler(IGamesStore store) 
        : IQueryHandler<FetchGamesQuery, Result<PagedEnumerable<Game>>> {
        public async Task<Result<PagedEnumerable<Game>>> Handle(FetchGamesQuery request, CancellationToken token) {
            var result = await store.FetchEntities(request.Page, request.Size, token);
            return result;
        }
    }
}
