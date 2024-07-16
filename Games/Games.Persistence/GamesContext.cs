using General.Domain;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Games.Persistence {
    public class GamesContext(ISender sender) : DbContext {
        public async override Task<int> SaveChangesAsync(CancellationToken token = default) {
            var events = ChangeTracker.Entries<Entity>()
                 .Select(x => x.Entity)
                 .SelectMany(x => x.Events);

            foreach (var notification in events) {
                await sender.Send(notification, token);
            }

            return await base.SaveChangesAsync(token);
        }
    }
}
