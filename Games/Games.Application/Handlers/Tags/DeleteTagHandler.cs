using Games.Contracts.Requests.Tags;
using Games.Domain.Tags;
using Games.Domain.Tags.Specifications;
using General.Contracts;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Tags {
    internal class DeleteTagHandler(ITagsStore store, IUnitOfWork unitOfWork) 
        : ICommandHandler<DeleteTagCommand, Result> {
        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken token) {
            var specification = new FindTag(request.Id);
            var result = await store.Fetch(specification, token);
            if (result.IsFailure) return result.Error;

            var tag = result.Value;
            store.Delete(tag);

            await unitOfWork.SaveChanges(token);

            return Result.Success;
        }
    }
}
