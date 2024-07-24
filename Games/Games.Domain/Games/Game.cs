using Games.Domain.Tags;
using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;
using System.Net;

namespace Games.Domain.Games {
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

        public static Result<Game> Create(string title, string description, decimal priceValue, IEnumerable<Tag> tags) {
            var errors = new ErrorsCollection();

            var priceResult = Price.CreatePrice(priceValue);
            if (!priceResult.IsSuccess) errors.Append(priceResult.Error.Messages);

            if (string.IsNullOrWhiteSpace(title) || title.Length > MaxTitleLength) {
                errors.Append(
                    title: "Invalid title!", 
                    description: $"Title is empty or has greater than {MaxTitleLength} symbols",
                    code: HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(description)) {
                errors.Append(title: "Empty description", code: HttpStatusCode.BadRequest);
            }

            if (errors.NotEmpty()) return FailureResult.Create(errors);

            var game = new Game(title, description, priceResult.Value, tags);
            return game;
        }
    }
}
