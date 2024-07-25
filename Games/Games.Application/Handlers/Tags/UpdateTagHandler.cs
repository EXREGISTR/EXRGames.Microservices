using Games.Contracts.Requests.Tags;
using Games.Domain.Tags;
using Games.Domain.Tags.Specifications;
using General.Contracts;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Tags {
    internal class UpdateTagHandler(ITagsStore store, IUnitOfWork unitOfWork) 
        : ICommandHandler<UpdateTagCommand, Result> {
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken token) {
            var specification = new FindTag(request.Id);
            var result = await store.Fetch(specification, token);

            if (result.IsFailure) return result.Error;

            var tag = result.Value;
            tag.Update(request.NewName);

            store.Update(tag);
            await unitOfWork.SaveChanges(token);

            return Result.Success;
        }
    }
}
