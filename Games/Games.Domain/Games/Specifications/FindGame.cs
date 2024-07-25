using General.Domain.Specifications;

namespace Games.Domain.Game.Specifications {
    public class FindGame : Specification<Game> {
        public FindGame(int id) {
            Where(x => x.Id == id);
        }

        public FindGame WithTags() {
            Include(x => x.Tags);
            return this;
        }
    }
}
