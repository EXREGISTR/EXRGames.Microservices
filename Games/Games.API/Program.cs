using Games.Application.Extensions;
using Games.Persistence.Extensions;
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();

            services.AddPersistence();
            services.AddApplication();
        }

        private static void ConfigureApp(WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.Services.EnsureAppliedMigrations();
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
