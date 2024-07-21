using Games.Domain.Games;
using General.Domain;

namespace Games.Contracts.Responses.Games {
    public record struct GamesResponse(PagedEnumerable<Game> Games);
}
