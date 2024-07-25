using General.Domain.Contracts;
using General.Domain.Results;
using General.Domain.Specifications;
using System.Linq.Expressions;

namespace General.Domain.Extensions {
    public static class StoresExtensions {
        public static Task<Result<IEnumerable<T>>> FetchEntities<T>(
            this IStore<T> source, CancellationToken token = default)
            where T : IAggregateRoot
            => source.FetchEntities(Specification<T>.Empty, token);

        public static Task<Result<PagedEnumerable<T>>> FetchEntities<T>(
            this IPaginableStore<T> source, int page, int size, CancellationToken token = default)
            where T : IAggregateRoot
            => source.FetchEntities(Specification<T>.Empty, page, size, token);

        public static Task<bool> Exists<T>(
            this IStore<T> source, Expression<Func<T, bool>> predicate, CancellationToken token = default)
            where T : IAggregateRoot
            => source.Exists(new WhereSpecification<T>(predicate), token);

        public async static Task<bool> Exists<T>(
            this IStore<T> source, ISpecification<T> specification, CancellationToken token = default)
            where T : IAggregateRoot
            => (await source.Fetch(specification, token)).IsSuccess;
    }
}
