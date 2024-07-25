using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;
using System.Net;

namespace Games.Domain.Tags {
    public class Tag : Entity<int>, IAggregateRoot {
        public string Name { get; private set; } = string.Empty;

        private Tag() { }
        private Tag(string name) => Name = name;

        public static Result<Tag> Create(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                var error = Error.Create(
                    title: "Invalid tag!", 
                    details: "Tag shouldn't has empty characters");
                return FailureResult.Create(error, HttpStatusCode.BadRequest);
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
