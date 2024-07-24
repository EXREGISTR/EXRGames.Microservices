using System.Net;

namespace General.Domain.Results {
    public readonly record struct Error {
        public string Title { get; }
        public string Description { get; }
        public HttpStatusCode Code { get; }

        private Error(string title, string description, HttpStatusCode code) {
            Title = title;
            Description = description;
            Code = code;
        }

        public static Error Create(string title = "Error", string description = "Error has occurred",
            HttpStatusCode code = HttpStatusCode.InternalServerError) 
            => new(title, description, code);
    }
}
