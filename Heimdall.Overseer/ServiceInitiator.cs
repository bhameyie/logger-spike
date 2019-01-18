using System;
using System.IO;
using System.Reflection;
using Autofac;
using Heimdall.Transport;
using Heimdall.Transport.Interfaces;
using Heimdall.Transport.RabbitMQ;
using log4net;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Overseer
{
    /// <summary>
    /// Assists in easily configuring a Service
    /// </summary>
    /// <remarks>It's IDisposable to ensure resources gets released even in case of an exception. For that to work, it *must* be used in a using statement</remarks>
    public class ServiceInitiator : IDisposable
    {
        private IConfiguredTransport _configuredTransport;
        private readonly ILog _logger;

        public TransportConfigurator Configurator { get; }

        public ServiceInitiator()
        {
            _logger = LogManager.GetLogger(typeof(ServiceInitiator));
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.{env.EnvironmentName}.json")
                .Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(config).As<IConfiguration>();

            containerBuilder.RegisterConsumers(Assembly.GetExecutingAssembly());


            Configurator = new TransportConfigurator(containerBuilder, config)
                .WithAgent<RabbitMqConfigurationAgent>();
        }


        public void Init()
        {
            _configuredTransport = Configurator.Configure();

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