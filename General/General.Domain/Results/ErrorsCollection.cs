namespace General.Domain.Results {
    public readonly record struct ErrorsCollection {
        private readonly List<Error> errors;

        public ErrorsCollection() => errors = [];
        public ErrorsCollection(List<Error> other) => errors = other;

        public bool NotEmpty => errors.Count > 0;
        public IEnumerable<Error> Collection => errors;

        public void Append(Error error) => errors.Add(error);
        public void Append(IEnumerable<Error> other) => errors.AddRange(other);
    }
}
