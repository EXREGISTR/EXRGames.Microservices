using System.Net;

namespace General.Domain.Results {
    public readonly record struct FailureResult(
        IEnumerable<Error> Messages, 
        HttpStatusCode StatusCode) {
        public static FailureResult Create(Error error, HttpStatusCode code) 
            => new([error], code);

        public static FailureResult Create(ErrorsCollection collection, HttpStatusCode code)
            => new(collection.Collection, code);

        public static FailureResult InternalServerError(string message)
            => Create(Error.Create(title: "Internal server error", details: message),
                HttpStatusCode.InternalServerError);
    }
}
