using Games.Domain.Games;
using General.Domain.Contracts;

namespace Games.Domain.UserGames {
    public class UserGame : IAggregateRoot {
        public string UserId { get; set; } = string.Empty;
        public int GameId { get; set; }

        public Game Game { get; set; } = null!;
    }
}
