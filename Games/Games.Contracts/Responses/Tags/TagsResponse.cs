using Games.Domain.Tags;
using General.Domain;

namespace Games.Contracts.Responses.Tags {
    public record struct TagsResponse(PagedEnumerable<Tag> Tags);
}
