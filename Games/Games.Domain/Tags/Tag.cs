using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;

namespace Games.Domain.Tags {
    public class Tag : Entity<int>, IAggregateRoot {
        public string Name { get; private set; } = string.Empty;

        private Tag() { }
        private Tag(string name) => Name = name;

        public static Result<Tag> Create(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                var error = Error.Create(title: "Empty name!");
                return FailureResult.Create(error);
            }

            var tag = new Tag(name.ToLower());

            return tag;
        }

        public void Update(string newName) {
            if (Name == newName) return;

            Name = newName;
        }
    }
}
