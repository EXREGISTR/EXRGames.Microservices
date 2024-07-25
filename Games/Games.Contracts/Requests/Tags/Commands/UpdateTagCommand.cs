using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Tags {
    public record UpdateTagCommand(int Id, string NewName) : ICommand<Result>;
}
