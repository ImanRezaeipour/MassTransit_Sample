using Command.Core;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Threading.Tasks;

namespace CommandReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CommandReceiver");
            RunMassTransitReceiverWithRabbit();
            Console.ReadLine();
        }



        private static void RunMassTransitReceiverWithRabbit()
        {
            IBusControl rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
            {
                IRabbitMqHost rabbitMqHost = rabbit.Host(new Uri(ConfigurationBus.URI), settings =>
                {
                    settings.Password("guest");
                    settings.Username("guest");
                });

                rabbit.ReceiveEndpoint(rabbitMqHost, ConfigurationBus.QueueName, conf =>
                {
                    conf.Consumer<RegisterCustomerConsumer>();
                });
            });

            rabbitBusControl.Start();
            Console.ReadKey();

            rabbitBusControl.Stop();
        }
    }

    public class RegisterCustomerConsumer : IConsumer<MessageClass>
    {
        public Task Consume(ConsumeContext<MessageClass> context)
        {
            Console.WriteLine("The command Received" + context.Message.Message);
            return Task.CompletedTask;
        }
    }
}
