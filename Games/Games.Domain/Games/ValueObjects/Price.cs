using General.Domain.Results;
using System.Net;

namespace Games.Domain.Game {
    public record Price {
        public decimal Value { get; private set; }

        private Price() { }
        private Price(decimal value) {
            Value = value;
        }

        internal static Result<Price> Create(decimal value) {
            if (value < 0) {
                var error = Error.Create(
                    title: "Price",
                    details: "Price should be greater than zero, or equals zero (if entity is free)");

                return FailureResult.Create(error, HttpStatusCode.BadRequest);
            }

            return new Price(value);
        }
    }
}
