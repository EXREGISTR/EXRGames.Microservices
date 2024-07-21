using General.Contracts;

namespace Games.Contracts.Requests.Tags {
    public record DeleteTagByNameCommand(string Name) : ICommand;
    public record DeleteTagByIdCommand(int Id) : ICommand;
}
