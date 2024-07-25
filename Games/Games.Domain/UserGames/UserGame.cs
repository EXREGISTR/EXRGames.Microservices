using Games.Domain.Game;
using General.Domain.Contracts;

namespace Games.Domain.UserGames {
    public class UserGame : IAggregateRoot {
        public string UserId { get; private set; } = string.Empty;
        public int GameId { get; private set; }

        public Game.Game Game { get; private set; } = null!;
    }
}
