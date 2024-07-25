using General.Domain.Specifications;

namespace Games.Domain.Tags.Specifications {
    public class FindConcreteTags : Specification<Tag> {
        public FindConcreteTags(IEnumerable<string> tags) {
            Where(tag => tags.Contains(tag.Name));
        }
    }
}