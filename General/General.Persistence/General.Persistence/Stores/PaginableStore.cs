using General.Domain;
using General.Domain.Contracts;
using General.Domain.Results;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace General.Persistence.Stores {
    public abstract class PaginableStore<T>(DbContext context)
        : Store<T>(context), IPaginableStore<T> where T : class, IAggregateRoot {
        public async virtual Task<Result<PagedEnumerable<T>>> FetchEntities(
            ISpecification<T> specification, int page, int size, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            var entities = table
                .Skip((page - 1) * size)
                .Take(size);

            try {
                var totalCount = await table.CountAsync(token);
                var paged = new PagedEnumerable<T>(
                    await entities.ToListAsync(token), page, size, totalCount);

                return paged;
            } catch (Exception exception) {
                return FailureResult.InternalServerError(exception.Message);
            }
        }
    }
}
