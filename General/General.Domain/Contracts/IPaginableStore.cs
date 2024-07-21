﻿namespace General.Domain.Contracts {
    public interface IPaginableStore<T> : IStore<T> where T : IAggregateRoot {
        public Task<PagedEnumerable<T>> FetchEntities(
            ISpecification<T> specification, int page, int size, CancellationToken token = default);
    }
}
