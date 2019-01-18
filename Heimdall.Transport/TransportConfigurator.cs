using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Heimdall.Transport.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Transport
{
    /// <summary>
    /// Configures any Heimdall supported transports
    /// </summary>
    /// <remarks>Only supports Autofac for DI</remarks>
    public sealed class TransportConfigurator
    {
        private readonly List<IConfigurationAgent> _agents;

        public ContainerBuilder Container { get; }
        public IConfiguration Configuration { get; }

        public TransportConfigurator(ContainerBuilder container, IConfiguration configuration)
        {
            Container = container;
            Configuration = configuration;
            _agents = new List<IConfigurationAgent>();
        }

        public TransportConfigurator WithAgent(IConfigurationAgent agent)
        {
            _agents.Add(agent);
            return this;
        }

        public TransportConfigurator WithAgent<T>() where T:IConfigurationAgent,new()
        {
            _agents.Add(new T());
            return this;
        }

        /// <summary>
        /// Configures the transport
        /// </summary>
        /// <returns></returns>
        public IConfiguredTransport Configure()
        {
            //todo: Include default agents instead, such as one for configuring retries and retry queues
            if (!_agents.Any())
            {
                throw new ApplicationException("No Configuration Agent specified");
            }

            _agents.ForEach(agent => agent.Configure(this));
            return new ConfiguredTransport(Container.Build(), _agents);
        }

        private class ConfiguredTransport : IConfiguredTransport
        {
            private readonly IEnumerable<IConfigurationAgent> _agents;

            public IContainer BuiltContainer { get; }
            public IHeimdallGateway Gateway { get; }


            public ConfiguredTransport(IContainer container, IEnumerable<IConfigurationAgent> agents)
            {
                BuiltContainer = container;
                Gateway = container.Resolve<IHeimdallGateway>();
                _agents = agents;
            }
            
            public void Start()
            {
                foreach (var configurationAgent in _agents)
                {
                    configurationAgent.OnStart(this);
                }
            }
            
            public void Dispose()
            {
                foreach (var configurationAgent in _agents)
                {
                    configurationAgent.OnRelease(this);
                }
            }
        }
    }
}
