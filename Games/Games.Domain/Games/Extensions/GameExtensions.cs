using Games.Domain.Tags;
using General.Domain.Results;

namespace Games.Domain.Game {
    public static class GameExtensions {
        public static Result Update(this Game game, 
            string title, string description, decimal price, IEnumerable<Tag> tags) {
            var errorsCollection = new ErrorsCollection();

            if (!game.UpdateTitle(title, out var titleError)) {
                errorsCollection.Append(titleError!.Value);
            }

            if (!game.UpdateDescription(description, out var descriptionError)) {
                errorsCollection.Append(descriptionError!.Value);
            }

            if (!game.UpdatePrice(price, out var priceError)) {
                errorsCollection.Append(priceError!.Value.Messages);
            }

            game.UpdateTags(tags);

            if (errorsCollection.NotEmpty) {
                return FailureResult.Create(errorsCollection, System.Net.HttpStatusCode.BadRequest);
            }

            return Result.Success;
        }

    }
}
