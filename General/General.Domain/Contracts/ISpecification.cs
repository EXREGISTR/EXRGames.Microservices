using System.Linq.Expressions;

namespace General.Domain.Contracts {
    public interface ISpecification<TEntity> where TEntity : IAggregateRoot {
        public IEnumerable<Expression<Func<TEntity, bool>>> WhereExpressions { get; }
        public IEnumerable<(Expression<Func<TEntity, object>> Expression, bool ByDescending)>
            OrdersSettings { get; }
        public IEnumerable<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    }
}
