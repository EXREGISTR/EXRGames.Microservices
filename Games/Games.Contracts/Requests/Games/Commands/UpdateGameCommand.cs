using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Games {
    public record UpdateGameCommand(
        int Id,
        string Title,
        string Description,
        decimal Price,
        IEnumerable<string> Tags) : ICommand<Result>;
}
