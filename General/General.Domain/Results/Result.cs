﻿namespace General.Domain.Results {
    public readonly record struct Result<T> {
        private readonly T? value;
        private readonly FailureResult? error;

        public T Value 
            => value ?? throw new NullReferenceException("Impossible get the value if exists errors!");
        
        public FailureResult Error
            => IsFailure ? error!.Value : throw new NullReferenceException("No errors!");

        private Result(T value) {
            this.value = value;
        }

        private Result(FailureResult error) {
            this.error = error;
        }

        public bool IsSuccess => !IsFailure;
        public bool IsFailure => error != null && error.Value.Messages.Any();

        public static Result<T> Success(T value)
            => new(value);

        public static Result<T> Failure(FailureResult error)
            => new(error);

        public static implicit operator Result<T>(T value)
            => Success(value);

        public static implicit operator Result<T>(FailureResult error)
            => Failure(error);
    }

    public readonly record struct Result {
        private readonly FailureResult? error;

        public FailureResult Error
            => IsFailure ? error!.Value : throw new NullReferenceException("No errors!");
        
        public bool IsSuccess => !IsFailure;
        public bool IsFailure => error != null && error.Value.Messages.Any();

        private Result(FailureResult error) => this.error = error;

        public static Result Success => default;
        public static Result Failure(FailureResult result) => new(result);

        public static implicit operator Result(FailureResult error) => Failure(error);
    }
}
