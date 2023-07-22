using MassTransit;
using System;

namespace Command.Core
{
    public static class ConfigurationBus
    {
        public static string URI = "rabbitmq://localhost";
        public static string QueueName = "TestQueue";
        public static IBusControl Configur()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(URI), h =>
                {
                    h.Username("guest");
                    h.Password("guest");

                });
            });
            return bus;
        }
    }
}
