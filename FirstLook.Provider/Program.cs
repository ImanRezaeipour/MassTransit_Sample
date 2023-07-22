using MassTransit;
using System;

namespace FirstLook.Provider
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LoginDateTime { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                   
                });
                
                sbc.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.Handler<UserData>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received: {context.Message.FirstName} {context.Message.LastName} {context.Message.LoginDateTime}");
                    });
                });
            });

            bus.Start(); 

            bus.Publish(new UserData { FirstName = "Alireza",LastName="Oroumand",LoginDateTime=DateTime.Now });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
