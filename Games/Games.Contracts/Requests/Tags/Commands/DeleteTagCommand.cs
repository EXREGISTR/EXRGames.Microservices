using General.Contracts;
using General.Domain.Results;

namespace Games.Contracts.Requests.Tags {
    public record DeleteTagCommand(int Id) : ICommand<Result>;
}
