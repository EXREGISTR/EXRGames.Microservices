using General.Domain.Specifications;

namespace Games.Domain.UserGames.Specifications
{
    public class FetchUserGame : Specification<UserGame> {
        public FetchUserGame(string userId, int gameId) {
            Where(x => x.UserId == userId && x.GameId == gameId);
        }

        public void WithGame() => Include(x => x.Game);
    }
}
