using Games.Contracts.Requests.Games;
using Games.Domain.Game;
using Games.Domain.Game.Specifications;
using Games.Domain.Tags;
using Games.Domain.Tags.Specifications;
using General.Contracts;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Application.Handlers.Games {
    internal class UpdateGameHandler(
        IGamesStore gamesStore,
        ITagsStore tagsStore,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateGameCommand, Result> {
        public async Task<Result> Handle(UpdateGameCommand request, CancellationToken token) {
            var gameResult = await gamesStore.Fetch(new FindGame(request.Id), token);

            if (gameResult.IsFailure) return gameResult.Error;

            var tagsResult = await tagsStore.FetchEntities(new FindConcreteTags(request.Tags), token);

            if (tagsResult.IsFailure) return tagsResult.Error;

            var updatingResult = gameResult.Value.Update(
                request.Title, request.Title, request.Price, tagsResult.Value);

            if (updatingResult.IsFailure) return gameResult.Error;

            await unitOfWork.SaveChanges(token);
            return Result.Success;
        }
    }
}
