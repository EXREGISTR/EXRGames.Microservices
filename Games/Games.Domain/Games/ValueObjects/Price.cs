using General.Domain.Results;
using System.Net;

namespace Games.Domain.Games {
    public record Price {
        public decimal Value { get; private set; }

        private Price() { }
        private Price(decimal value) {
            Value = value;
        }

        internal static Result<Price> CreatePrice(decimal value) {
            if (value < 0) {
                var error = Error.Create(
                    title: "Price",
                    description: "Price should be greater than zero, or equals zero (if entity is free)",
                    code: HttpStatusCode.BadRequest);

                return FailureResult.Create(error);
            }

            return new Price(value);
        }
    }
}
