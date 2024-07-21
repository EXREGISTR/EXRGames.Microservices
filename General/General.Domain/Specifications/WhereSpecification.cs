using General.Domain.Contracts;
using System.Linq.Expressions;

namespace General.Domain.Specifications {
    public class WhereSpecification<T>
        (Expression<Func<T, bool>> predicate) : ISpecification<T> where T : IAggregateRoot {
        public IEnumerable<Expression<Func<T, bool>>> WhereExpressions => [predicate];

        public IEnumerable<(Expression<Func<T, object>> Expression, bool ByDescending)> OrdersSettings
            => [];

        public IEnumerable<Expression<Func<T, object>>> IncludeExpressions
            => [];
    }
}
