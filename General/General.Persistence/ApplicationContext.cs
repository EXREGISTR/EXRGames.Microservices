using MediatR;
using Microsoft.EntityFrameworkCore;

namespace General.Persistence {
    public abstract class ApplicationContext(IMediator mediator) : DbContext {
        
        public async virtual override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            await mediator.DispatchDomainEventsAsync(this);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
