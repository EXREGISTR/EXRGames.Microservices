using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Tags {
    public record CreateTagCommand(string Name) : ICommand<Result<int>>;
}
