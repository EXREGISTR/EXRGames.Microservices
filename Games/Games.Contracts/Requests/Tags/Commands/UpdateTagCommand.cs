using General.Contracts;

namespace Games.Contracts.Requests.Tags {
    public record UpdateTagCommand(int Id, string NewName) : ICommand;
}
