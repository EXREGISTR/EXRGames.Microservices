namespace General.Domain.Results {
    public readonly record struct FailureResult(IEnumerable<Error> Messages) {
        public static FailureResult Create(Error error) => new([error]);
        public static FailureResult Create(ErrorsCollection collection) => new(collection.Collection);
    }
}
