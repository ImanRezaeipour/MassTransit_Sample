using Command.Core;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EventReceiver01
{
    class SomethingHappenedConsumer : IConsumer<MessageClass>
    {
        public Task Consume(ConsumeContext<MessageClass> context)
        {
            Console.WriteLine("TXT: " + context.Message.Message);
            return Task.CompletedTask;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EventReceiver01");
          
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri(ConfigurationBus.URI), h => { });

                x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", e =>
                  e.Consumer<SomethingHappenedConsumer>());
            });
            bus.Start();
            
            Console.ReadKey();
            bus.Stop();
        }
    }
}
