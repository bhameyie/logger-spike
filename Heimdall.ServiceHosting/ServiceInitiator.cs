using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Heimdall.Transport;
using Heimdall.Transport.Interfaces;
using log4net;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Heimdall.ServiceHosting
{
    /// <summary>
    /// Assists in easily configuring a Service
    /// </summary>
    /// <remarks>It's IDisposable to ensure resources gets released even in case of an exception. For that to work, it *must* be used in a using statement</remarks>
    public class ServiceInitiator : IDisposable
    {
        private IConfiguredTransport _configuredTransport;
        private readonly ILog _logger;


        public ServiceInitiator(IEnumerable<IConfigurationAgent> agents, IModule[] modules)
        {
            _logger = LogManager.GetLogger(typeof(ServiceInitiator));
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(config).As<IConfiguration>();

            containerBuilder.RegisterConsumers(Assembly.GetExecutingAssembly());
            
            foreach (var module in modules)
            {
                containerBuilder.RegisterModule(module);
            }


            var configurator = agents.Aggregate(new TransportConfigurator(containerBuilder, config),
                (c, a) => c.WithAgent(a));
            
            _configuredTransport = configurator.Configure();

            _configuredTransport.Start();

            _logger.Info("Started the transport");
        }


        public void Dispose()
        {
            _configuredTransport?.Dispose();

            _logger.Info("Transport disposed");
        }
    }
}