using Games.Domain.Tags;
using General.Contracts;
using General.Domain;
using General.Domain.Results;

namespace Games.Contracts.Requests.Tags {
    public class FetchTagsQuery : PaginableQuery, IQuery<Result<PagedEnumerable<Tag>>>;
}
