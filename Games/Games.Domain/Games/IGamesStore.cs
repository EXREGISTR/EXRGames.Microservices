using General.Domain.Contracts;

namespace Games.Domain.Games {
    public interface IGamesStore : IPaginableStore<Game>;
}
