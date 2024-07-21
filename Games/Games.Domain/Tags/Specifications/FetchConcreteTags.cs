using General.Domain.Specifications;

namespace Games.Domain.Tags.Specifications {
    public class FetchConcreteTags : Specification<Tag> {
        public FetchConcreteTags(IEnumerable<string> tags) {
            Where(tag => tags.Contains(tag.Name));
        }
    }
}