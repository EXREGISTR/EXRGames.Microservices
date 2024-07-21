using General.Domain.Contracts;
using MediatR;

namespace General.Domain {
    public abstract class Entity : IEntity {
        private readonly List<INotification> events = [];

        public IEnumerable<INotification> RetrieveEvents() {
            var events = this.events;
            return events;
        }

        public void ClearDomainEvents() => events.Clear();

        protected void AddEvent(INotification notification) => events.Add(notification);
        protected void RemoveEvent(INotification notification) => events.Remove(notification);
    }

    public abstract class Entity<TId> : Entity {
        public TId Id { get; protected set; } = default!;

        public override bool Equals(object? obj) {
            if (obj == null) return false;

            if (obj is not Entity<TId> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode() => Id!.GetHashCode();

        public static bool operator ==(Entity<TId> left, Entity<TId> right) {
            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right) {
            return !(left == right);
        }

        private bool Equals(Entity<TId> other) {
            if (ReferenceEquals(this, other)) return true;

            return other.Id!.Equals(Id);
        }
    }
}
