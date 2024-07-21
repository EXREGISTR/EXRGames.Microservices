using MediatR;

namespace General.Domain.Contracts {
    public interface IEntity {
        public IEnumerable<INotification> RetrieveEvents();
        public void ClearDomainEvents();
    }
}
