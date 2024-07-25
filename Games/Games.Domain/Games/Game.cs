using Games.Domain.Tags;
using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;
using MediatR;
using System.Net;

namespace Games.Domain.Game {
    public class Game : Entity<int>, IAggregateRoot {
        public const int MaxTitleLength = 100;

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Price Price { get; private set; } = null!;

        public IEnumerable<Tag> Tags { get; private set; } = [];

        private Game() { }

        private Game(string title, string description, Price price, IEnumerable<Tag> tags) {
            Title = title;
            Description = description;
            Price = price;
            Tags = tags;
        }

        public bool UpdateTitle(string newTitle, out Error? error) {
            error = null;
            if (Title == newTitle) return true;

            if (!IsValidTitle(newTitle, out error)) return false;

            Title = newTitle;
            return true;
        }

        public bool UpdateDescription(string newDescription, out Error? error) {
            error = null;
            if (Title == newDescription) return true;

            if (!IsValidDescription(newDescription, out error)) return false;

            Title = newDescription;
            return true;
        }

        public void UpdateTags(IEnumerable<Tag> tags) {
            if (Tags.SequenceEqual(tags)) return;

            Tags = tags;
        }

        public bool UpdatePrice(decimal newPriceValue, out FailureResult? error) {
            error = null;
            if (Price.Value == newPriceValue) return true;

            var result = Price.Create(newPriceValue);
            if (result.IsFailure) {
                error = result.Error;
                return false;
            }

            Price = result.Value;
            return true;
        }

        private static bool IsValidTitle(string title, out Error? error) {
            var result = !string.IsNullOrWhiteSpace(title) && title.Length <= MaxTitleLength;
            error = null;

            if (!result) {
                error = Error.Create(
                    title: "Invalid title",
                    details: $"Title is empty or has greater than {MaxTitleLength} symbols");
            }

            return result;
        }

        private static bool IsValidDescription(string description, out Error? error) {
            var result = string.IsNullOrWhiteSpace(description);
            error = null;

            if (!result) {
                error = Error.Create(title: "Invalid description");
            }

            return result;
        }

        public static Result<Game> Create(string title, string description, decimal priceValue, IEnumerable<Tag> tags) {
            var errors = new ErrorsCollection();

            var priceResult = Price.Create(priceValue);
            if (priceResult.IsFailure) errors.Append(priceResult.Error.Messages);

            if (!IsValidTitle(title, out var titleError)) {
                errors.Append(titleError!.Value);
            }

            if (!IsValidDescription(description, out var descriptionError)) {
                errors.Append(descriptionError!.Value);
            }

            if (errors.NotEmpty) return FailureResult.Create(errors, HttpStatusCode.BadRequest);

            var game = new Game(title, description, priceResult.Value, tags);
            return game;
        }
    }
}
