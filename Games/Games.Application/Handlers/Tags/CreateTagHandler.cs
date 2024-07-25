using Games.Contracts.Requests.Tags;
using Games.Domain.Tags;
using General.Contracts;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Tags {
    internal class CreateTagHandler(ITagsStore store, IUnitOfWork unitOfWork) 
        : ICommandHandler<CreateTagCommand, Result<int>> {
        public async Task<Result<int>> Handle(CreateTagCommand request, CancellationToken token) {
            var result = Tag.Create(request.Name);
            if (result.IsFailure) return result.Error;

            var tag = result.Value;
            store.Create(tag);
            await unitOfWork.SaveChanges(token);

            return tag.Id;
        }
    }
}
