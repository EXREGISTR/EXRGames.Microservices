using Games.Domain.Games;
using MediatR;

namespace Games.Contracts.Requests.Games {
    public class FetchGameQuery : IRequest<Game> {
        public int Id { get; set; }
    }
}
