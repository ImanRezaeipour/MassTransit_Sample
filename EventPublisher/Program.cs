using Command.Core;
using MassTransit;
using System;

namespace EventPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EventPublisher");

            var bus = ConfigurationBus.Configur();
            bus.Start();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Please Enter a text");
                var message = new MessageClass
                {
                    Message = Console.ReadLine()
                };
                bus.Publish<MessageClass>(message);
            }
            bus.Stop();
        }
    }
}
