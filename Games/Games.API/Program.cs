using Games.Persistence.Extensions;
using Games.Application.Extensions;
using General.Persistence;

namespace Games.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);

            var app = builder.Build();
            ConfigureApp(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddPersistence();
            services.AddApplication();
        }

        private static void ConfigureApp(WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Services.EnsureAppliedMigrations();
        }
    }
}
