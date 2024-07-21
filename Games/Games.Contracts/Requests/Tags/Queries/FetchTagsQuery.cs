using Games.Contracts.Responses.Tags;
using General.Contracts;

namespace Games.Contracts.Requests.Tags {
    public record FetchTagsQuery : IQuery<TagsResponse>;
}
