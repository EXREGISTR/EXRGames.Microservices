using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Games {
    public record DeleteGameCommand(int Id) : ICommand<Result>;
}
