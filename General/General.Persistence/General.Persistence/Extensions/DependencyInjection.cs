using General.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace General.Persistence {
    public static class DependencyInjection {
        public static void AddDatabaseContext<TContext>(this IServiceCollection services) 
            where TContext : DbContext, IUnitOfWork {
            services.AddDbContext<TContext>(optionsLifetime: ServiceLifetime.Singleton);
            services.AddScoped<IUnitOfWork, TContext>(x => x.GetRequiredService<TContext>());
            services.AddScoped<DbContext, TContext>(x => x.GetRequiredService<TContext>());
        }
    }
}
