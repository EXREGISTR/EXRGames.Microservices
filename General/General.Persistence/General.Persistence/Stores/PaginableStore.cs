using General.Domain;
using General.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace General.Persistence.Stores {
    public abstract class PaginableStore<T>(DbContext context)
        : Store<T>(context), IPaginableStore<T> where T : class, IAggregateRoot {
        public async virtual Task<PagedEnumerable<T>> FetchEntities(
            ISpecification<T> specification, int page, int size, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            var entities = table
                .Skip((page - 1) * size)
                .Take(size);

            var totalCount = await table.CountAsync(token);
            return new(await entities.ToListAsync(token), page, size, totalCount);
        }
    }
}
