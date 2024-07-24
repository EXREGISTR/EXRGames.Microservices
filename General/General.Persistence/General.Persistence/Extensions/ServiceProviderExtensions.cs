using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace General.Persistence {
    public static class ServiceProviderExtensions {
        public static void EnsureAppliedMigrations(this IServiceProvider services) {
            using var scope = services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DbContext>();

            context.Database.Migrate();
        }
    }
}
