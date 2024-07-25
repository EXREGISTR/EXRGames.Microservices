using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Games {
    public record CreateGameCommand(
        string Title, 
        string Description, 
        decimal Price, 
        ICollection<string>? Tags) : ICommand<Result<int>>;
}
