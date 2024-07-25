using Games.Contracts.Requests.Games;
using Games.Domain.Game;
using Games.Domain.Game.Specifications;
using General.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Games {
    internal class FetchGameHandler(IGamesStore store) 
        : IQueryHandler<FetchGameQuery, Result<Game>> {
        public async Task<Result<Game>> Handle(FetchGameQuery request, CancellationToken token) {
            var specification = new FindGame(request.Id)
                .WithTags();

            var result = await store.Fetch(specification, token);
            return result;
        }
    }
}
