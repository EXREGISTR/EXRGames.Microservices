using General.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace General.Persistence.Stores {
    public abstract class Store<TEntity>(DbContext context) : IStore<TEntity> where TEntity : class, IAggregateRoot {
        protected DbSet<TEntity> Table => context.Set<TEntity>();

        public virtual void Create(TEntity entity) 
            => Table.Add(entity);

        public Task<TEntity?> Fetch(ISpecification<TEntity> specification, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            return table.FirstOrDefaultAsync(token);
        }

        public async virtual Task<IEnumerable<TEntity>> FetchEntities(ISpecification<TEntity> specification, CancellationToken token = default) {
            var table = Table.AsNoTracking()
                .ApplySpecification(specification);

            return await table.ToListAsync(token);
        }

        public virtual void Delete(TEntity entity) 
            => Table.Remove(entity);

        public virtual void Update(TEntity entity) 
            => context.Entry(entity).State = EntityState.Modified;
    }
}
