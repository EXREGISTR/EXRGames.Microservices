using System.Data;

namespace General.Domain.Contracts {
    public interface IUnitOfWork {
        public Task<int> SaveChangesAsync(CancellationToken token = default);
        public IDbTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
