using Games.Contracts.Requests.Tags;
using Games.Domain.Tags;
using General.Contracts;
using General.Domain;
using General.Domain.Extensions;
using General.Domain.Results;

namespace Games.Application.Handlers.Tags {
    internal class FetchTagsHandler(ITagsStore store)
        : IQueryHandler<FetchTagsQuery, Result<PagedEnumerable<Tag>>> {
        public async Task<Result<PagedEnumerable<Tag>>> Handle(FetchTagsQuery request, CancellationToken token) {
            var result = await store.FetchEntities(request.Page, request.Size, token);
            return result;
        }
    }
}
