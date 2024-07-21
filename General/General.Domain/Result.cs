namespace General.Domain {
    public readonly record struct Result<T> {
        public T? Value { get; }
        public string? Error { get; }

        internal Result(T? value, string? error) {
            Value = value;
            Error = error;
        }

        public bool IsSuccess => Error == null;

        public static Result<T> Success(T value) => new(value, null);
        public static Result<T> Failure(string error) => new(default, error);

        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator Result<T>(string error) => Failure(error);

    }

    public readonly record struct Result {
        public string? Error { get; }
        public bool IsSuccess => Error == null;

        internal Result(string? error) {
            Error = error;
        }

        public static readonly Result Success = new(null);
        public static Result Failure(string error) => new(error);

        public static implicit operator Result(string error) => Failure(error);
    }
}
