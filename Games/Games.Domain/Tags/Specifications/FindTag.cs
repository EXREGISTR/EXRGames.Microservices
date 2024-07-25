using General.Domain.Specifications;

namespace Games.Domain.Tags.Specifications {
    public class FindTag : Specification<Tag> {
        public FindTag(int id) {
            Where(x => x.Id == id);
        }
    }
}