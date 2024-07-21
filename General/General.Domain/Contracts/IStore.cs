namespace General.Domain.Contracts {
    public interface IStore<TEntity> where TEntity : IAggregateRoot {
        public void Create(TEntity entity);
        public Task<TEntity?> Fetch(ISpecification<TEntity> specification, CancellationToken token = default);
        public Task<IEnumerable<TEntity>> FetchEntities(ISpecification<TEntity> specification, CancellationToken token = default);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
