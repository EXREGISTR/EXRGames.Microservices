using Games.Domain.Games;
using General.Contracts;

namespace Games.Contracts.Requests.Games {
    public class FetchGameQuery : IQuery<Game> {
        public int Id { get; set; }
    }
}
