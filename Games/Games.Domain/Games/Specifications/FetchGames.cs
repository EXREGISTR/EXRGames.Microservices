using General.Domain.Specifications;
using System.Linq.Expressions;

namespace Games.Domain.Games.Specifications {
    public class FetchGames : Specification<Game> {
        public FetchGames(
            string? search,
            decimal? minPrice, decimal? maxPrice,
            IEnumerable<string>? tags,
            IEnumerable<GamesSortMethod>? sortMethods) {
            if (minPrice != null || maxPrice != null) {
                FilterByPrice(minPrice, maxPrice);
            }

            if (search != null) {
                FilterBySearch(search);
            }

            if (tags != null) {
                FilterByTags(tags);
            }

            if (sortMethods != null) {
                ApplySorting(sortMethods);
            }
        }

        private void FilterByPrice(decimal? minPrice, decimal? maxPrice) {
            minPrice ??= decimal.Zero;
            maxPrice ??= decimal.MaxValue;

            if (maxPrice == decimal.Zero) {
                Where(x => x.Price == decimal.Zero);
                return;
            }

            if (minPrice == maxPrice) {
                Where(game => game.Price == maxPrice);
                return;
            }

            if (minPrice > decimal.Zero) {
                Where(x => x.Price >= minPrice);
            }

            if (maxPrice < decimal.MaxValue) {
                Where(x => x.Price <= maxPrice);
            }
        }

        private void FilterBySearch(string search)
            => Where(x => x.Title.Contains(search));

        private void FilterByTags(IEnumerable<string> tags) {
            Expression<Func<Game, bool>> predicate =
                game => game.Tags.Any(tag => tags.Contains(tag.Name));

            Where(predicate);
        }

        private void ApplySorting(IEnumerable<GamesSortMethod> methods) {
            foreach (var method in methods) {
                OrderBy(GetOrderSelector(method.Type), method.ByDescending);
            }
        }

        private static Expression<Func<Game, object>> GetOrderSelector(GamesSortType type)
            => type switch {
                GamesSortType.Title => x => x.Title,
                GamesSortType.Price => x => x.Price,
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
    }

    public enum GamesSortType { Title, Price }
    public record GamesSortMethod(GamesSortType Type, bool ByDescending);

}
