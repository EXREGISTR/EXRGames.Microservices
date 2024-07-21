using General.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace General.Persistence {
    internal static class QueryableExtensions {
        public static IQueryable<TSource> ApplySpecification<TSource>(
            this IQueryable<TSource> source, ISpecification<TSource> specification)
            where TSource : class, IAggregateRoot {
            foreach (var expression in specification.WhereExpressions) {
                source = source.Where(expression);
            }

            foreach (var (expression, byDescending) in specification.OrdersSettings) {
                source = byDescending
                    ? source.OrderByDescending(expression)
                    : source.OrderBy(expression);
            }

            foreach (var expression in specification.IncludeExpressions) {
                source = source.Include(expression);
            }

            return source;
        }
    }
}
