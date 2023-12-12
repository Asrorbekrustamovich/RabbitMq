using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMq1.Domain;

namespace RabbitMq1
{
    public static class StartUp
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<PupilConsumer>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // Define your exchange
                    cfg.ExchangeType = ExchangeType.Direct;

                    // Configure the receive endpoint
                    cfg.ReceiveEndpoint("bindingque", e =>
                    {
                        // Configure the binding to the exchange
                        e.Bind("bindingexchange", b =>
                        {
                            // Configure other binding settings if needed
                            //b.RoutingKey = "your_routing_key";
                        });

                        // Register consumer
                        e.Consumer<PupilConsumer>();
                    });

                    // Configure other RabbitMQ settings as needed...
                });
            });

            services.AddMassTransitHostedService(true);
        }
    }
}
