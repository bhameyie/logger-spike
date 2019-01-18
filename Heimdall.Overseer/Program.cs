using System;
using System.Runtime.Loader;
using log4net.Core;
using Microsoft.Extensions.CommandLineUtils;

namespace Heimdall.Overseer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sighting Overseer");

            var app = new CommandLineApplication
            {
                Name = "Overseer",
                FullName = "Heimdall Sighting Overseer",
                Description = "A Reactive Endpoint user to monitor sightings and trigger investigations"
            };

            app.OnExecute(() =>
            {
                using (var initiator=new ServiceInitiator())
                {
                    initiator.Init();
                    Console.WriteLine("Overseer Started");
                    return Console.Read();
                }
            });
        }
    }
}
