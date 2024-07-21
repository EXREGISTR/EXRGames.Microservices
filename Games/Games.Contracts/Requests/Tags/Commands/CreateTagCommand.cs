using General.Contracts;

namespace Games.Contracts.Requests.Tags {
    public record CreateTagCommand(string Name) : ICommand;
}
