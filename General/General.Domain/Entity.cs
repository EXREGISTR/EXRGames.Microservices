using MediatR;

namespace General.Domain {
    public abstract class Entity {
        private List<INotification> events;
        public IEnumerable<INotification> Events => events ?? [];
    }
}
