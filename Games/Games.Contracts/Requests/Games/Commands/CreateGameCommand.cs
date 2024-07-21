using FluentValidation;
using Games.Domain.Games;
using General.Contracts;
using General.Domain;

namespace Games.Contracts.Requests.Games {
    public class CreateGameCommand : ICommand<Result<int>> {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<string>? Tags { get; set; }
    }
}
