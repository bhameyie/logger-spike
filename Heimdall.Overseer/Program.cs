using System;
using Autofac;
using Autofac.Core;
using Heimdall.DataAccess;
using Heimdall.DataAccess.MongoDb;
using Heimdall.DataAccess.MongoDb.Entities;
using Heimdall.ServiceHosting;
using Heimdall.Transport.Interfaces;
using Heimdall.Transport.RabbitMQ;
using Microsoft.Extensions.CommandLineUtils;

namespace Heimdall.Overseer
{
    class Program
    {
        static int Main(string[] args)
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
                var mqAgent = new RabbitMqConfigurationAgent()
                    .WithConsumer<SightingInvestigationConsumer<ReportedSighting>>();

                var agents = new IConfigurationAgent[] {mqAgent};
                var modules = new IModule[] {new MongoModule(), new OverSeerModule()};

                using (new ServiceInitiator(agents, modules))
                {
                    Console.WriteLine("Overseer Started");
                    return Console.Read();
                }
            });

            return app.Execute(args);
        }
    }
}