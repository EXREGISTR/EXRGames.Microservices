using General.Domain.Contracts;
using System.Linq.Expressions;

namespace General.Domain.Specifications {
    public abstract class Specification<T> : ISpecification<T> where T : IAggregateRoot {
        public static readonly ISpecification<T> Empty = new EmptySpecification();
        private class EmptySpecification : ISpecification<T> {
            public IEnumerable<Expression<Func<T, bool>>> WhereExpressions => [];
            public IEnumerable<Expression<Func<T, object>>> IncludeExpressions => [];

            public IEnumerable<(Expression<Func<T, object>> Expression, bool ByDescending)> OrdersSettings
                => [];
        }

        private readonly Queue<Expression<Func<T, bool>>> whereExpressions = [];
        private readonly Queue<Expression<Func<T, object>>> includeExpressions = [];
        private readonly Queue<(Expression<Func<T, object>> Expression, bool ByDescending)>
            orderSettingsExpressions = [];

        public IEnumerable<Expression<Func<T, bool>>> WhereExpressions
            => whereExpressions;

        public IEnumerable<Expression<Func<T, object>>> IncludeExpressions
            => includeExpressions;

        public IEnumerable<(Expression<Func<T, object>> Expression, bool ByDescending)> OrdersSettings
            => orderSettingsExpressions;

        protected void Where(Expression<Func<T, bool>> expression)
            => whereExpressions.Enqueue(expression);

        protected void Include(Expression<Func<T, object>> expression)
            => includeExpressions.Enqueue(expression);

        protected void OrderBy(Expression<Func<T, object>> expression, bool byDescending)
            => orderSettingsExpressions.Enqueue((expression, byDescending));
    }
}
