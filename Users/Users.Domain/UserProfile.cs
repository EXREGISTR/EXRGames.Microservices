using General.Domain;
using General.Domain.Contracts;

namespace Users.Domain {
    public class UserProfile : Entity<string>, IAggregateRoot {
        public string Nickname { get; private set; } = string.Empty;
        public DateOnly? BirthDate { get; private set; }
        public DateOnly DateOfRegistration { get; private set; }
    }
}
