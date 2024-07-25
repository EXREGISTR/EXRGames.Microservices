using Games.Domain.Game;
using Games.Domain.Game.Specifications;
using General.Contracts;
using General.Domain;
using General.Domain.Results;

namespace Games.Contracts.Requests.Games {
    public class FetchGamesQuery : PaginableQuery, IQuery<Result<PagedEnumerable<Game>>> {
        public string? Search { get; private set; }
        public string[]? Tags { get; private set; }
        public decimal MinPrice { get; private set; } = decimal.Zero;
        public decimal MaxPrice { get; private set; } = decimal.MaxValue;
        public GamesSortMethod[]? OrderTypes { get; private set; }
    }
}
