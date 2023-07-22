using Command.Core;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CommandSender
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CommandSender");
          
            RunMassTransitPublisherWithRabbit();
        }

        private static void RunMassTransitPublisherWithRabbit()
        {
            IBusControl rabbitBusControl = ConfigurationBus.Configur();
            Task<ISendEndpoint> sendEndpointTask = rabbitBusControl.GetSendEndpoint(new Uri(string.Concat(ConfigurationBus.URI, "/", ConfigurationBus.QueueName)));
            ISendEndpoint sendEndpoint = sendEndpointTask.Result;

            sendEndpoint.Send<MessageClass>(new MessageClass
            {
                Message = "Send Message From Class"
            });
            Console.ReadKey();
        }
    }
}
