using General.Domain.Contracts;
using General.Domain.Results;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace General.Persistence.Stores {
    public abstract class Store<TEntity>(DbContext context) : IStore<TEntity> where TEntity : class, IAggregateRoot {
        protected DbSet<TEntity> Table => context.Set<TEntity>();

        public virtual void Create(TEntity entity) => Table.Add(entity);

        public async Task<Result<TEntity>> Fetch(ISpecification<TEntity> specification, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            try {
                var entity = await table.FirstOrDefaultAsync(token);
                return entity != null
                    ? entity
                    : FailureResult.Create(Error.Create(
                        title: "Entity doesn't exists"),
                        HttpStatusCode.NotFound);

            } catch (Exception exception) {
                return FailureResult.InternalServerError(exception.Message);
            }
        }

        public async virtual Task<Result<IEnumerable<TEntity>>> FetchEntities(ISpecification<TEntity> specification, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            try {
                var entities = await table.ToListAsync(token);
                return entities;
            } catch (Exception exception) {
                return FailureResult.InternalServerError(exception.Message);
            }
        }

        public virtual void Delete(TEntity entity) 
            => Table.Remove(entity);

        public virtual void Update(TEntity entity) 
            => Table.Update(entity);
    }
}
