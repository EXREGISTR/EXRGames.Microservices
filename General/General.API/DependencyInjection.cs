using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace General.API {
    public static class DependencyInjection {
        public static void AddMassTransitWithRabbitMQ(this IServiceCollection services) {
            services.AddMassTransit(options => {
                options.AddConsumers(Assembly.GetEntryAssembly());

                options.UsingRabbitMq((context, options) => {
                    var configuration = context.GetRequiredService<IConfiguration>();

                    var serviceHost = configuration.GetRequiredSection(
                       "ServiceHost").Value;

                    var rabbitMQHost = configuration.GetRequiredSection(
                        "RabbitMQHost").Value;

                    options.Host(rabbitMQHost);
                    options.ConfigureEndpoints(context,
                        new KebabCaseEndpointNameFormatter(serviceHost, false));

                    options.UseMessageRetry(options => {
                        options.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });
        }
    }
}
