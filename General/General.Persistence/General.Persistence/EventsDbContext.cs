using General.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace General.Persistence {
    public abstract class EventsDbContext : DbContext, IUnitOfWork {
        private readonly ISender sender;

        protected EventsDbContext() => sender = null!;

        protected EventsDbContext(ISender sender) {
            this.sender = sender;
        }

        public IDbTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted) 
            => Database.BeginTransaction(level).GetDbTransaction();

        public async override Task<int> SaveChangesAsync(CancellationToken token = default) {
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
