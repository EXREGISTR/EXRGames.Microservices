using Games.Domain.Tags;
using General.Domain;
using General.Domain.Contracts;

namespace Games.Domain.Games {
    public class Game : Entity<int>, IAggregateRoot {
        public const int MaxTitleLength = 100;

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public IEnumerable<Tag> Tags { get; private set; } = [];

        private Game() { }

        private Game(string title, string description, decimal price, IEnumerable<Tag> tags) {
            Title = title;
            Description = description;
            Price = price;
            Tags = tags;
        }

        public static Result<Game> Create(string title, string description, decimal price, IEnumerable<Tag> tags) {
            if (string.IsNullOrWhiteSpace(title) || title.Length > MaxTitleLength) {
                return $"Invalid title! Title is empty or has greater than {MaxTitleLength} symbols";
            }

            if (string.IsNullOrWhiteSpace(description)) {
                return "Empty description";
            }

            if (price < 0) {
                return "Price should be greater than zero, or equals zero (if game is free)";
            }

            var game = new Game(title, description, price, tags);
            return game;
        }
    }
}
