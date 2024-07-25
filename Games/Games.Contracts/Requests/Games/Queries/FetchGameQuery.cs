using Games.Domain.Game;
using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Games {
    public class FetchGameQuery : IQuery<Result<Game>> {
        public int Id { get; private set; }
    }
}
