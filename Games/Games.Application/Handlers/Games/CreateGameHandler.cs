using Games.Contracts.Requests.Games;
using Games.Domain.Games;
using Games.Domain.Tags;
using Games.Domain.Tags.Specifications;
using General.Contracts;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Games {
    public class CreateGameHandler(
        IGamesStore gamesStore,
        ITagsStore tagsStore,
        IUnitOfWork unitOfWork) : ICommandHandler<CreateGameCommand, Result<int>> {
        public async Task<Result<int>> Handle(CreateGameCommand request, CancellationToken token) {
            IEnumerable<Tag> tags = [];

            if (request.Tags != null) {
                tags = await tagsStore.FetchEntities(new FetchConcreteTags(request.Tags), token);
            }

            var result = Game.Create(request.Title, request.Description, request.Price, tags);
            if (!result.IsSuccess) {
                return result.Error;
            }

            var game = result.Value;
            gamesStore.Create(game);

            await unitOfWork.SaveChanges(token);
            return game.Id;
        }
    }
}
