using General.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace General.Persistence {
    public abstract class EXRGamesDbContext : DbContext, IUnitOfWork {
        private readonly ISender sender;

        protected EXRGamesDbContext() => sender = null!;

        protected EXRGamesDbContext(ISender sender) {
            this.sender = sender;
        }

        public IDbTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted) 
            => Database.BeginTransaction(level).GetDbTransaction();

        public async Task<int> SaveChanges(CancellationToken token = default) {
            var events = ChangeTracker.Entries<IEntity>()
                 .Select(x => x.Entity)
                 .SelectMany(x => {
                     var events = x.RetrieveEvents();
                     x.ClearDomainEvents();

                     return events;
                 });

            var result = await base.SaveChangesAsync(token);

            foreach (var notification in events) {
                await sender.Send(notification, token);
            }

            return result;
        }
    }
}
