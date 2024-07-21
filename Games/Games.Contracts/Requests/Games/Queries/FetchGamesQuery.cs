using FluentValidation;
using Games.Contracts.Responses.Games;
using Games.Domain.Games.Specifications;
using General.Contracts;

namespace Games.Contracts.Requests.Games {
    public class FetchGamesQuery : PaginableQuery, IQuery<GamesResponse> {
        public string? Search { get; set; }
        public string[]? Tags { get; set; }
        public decimal MinPrice { get; set; } = decimal.Zero;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public GamesSortMethod[]? OrderTypes { get; set; }
    }

    public class FetchGamesValidator : AbstractValidator<FetchGamesQuery> {
        public FetchGamesValidator() {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Incorrect minimal price value");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Incorrect maximal price value");
        }
    }
}
