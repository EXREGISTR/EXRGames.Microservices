using Games.Contracts.Requests.Games;
using Games.Contracts.Responses.Games;
using Games.Domain.Games;
using General.Contracts;
using General.Domain;

namespace Games.Application.Handlers.Games {
    public class FetchGamesHandler(IGamesStore store) : IQueryHandler<FetchGamesQuery, GamesResponse> {
        public async Task<GamesResponse> Handle(FetchGamesQuery request, CancellationToken token) {
            var games = await store.FetchEntities(request.Page, request.Size, token);
            return new(games);
        }
    }
}
