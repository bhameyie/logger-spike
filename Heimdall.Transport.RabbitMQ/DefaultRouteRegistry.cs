using System;
using Heimdall.Transport.Interfaces;
using log4net;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Transport.RabbitMQ
{
    public class DefaultRouteRegistry : IRouteRegistry
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;

        public DefaultRouteRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetLogger(typeof(DefaultRouteRegistry));
        }

        public Uri For<T>() where T : IMessage
        {
            var typeName = typeof(T).FullName;
            var query = $"Heimdall:Routes:{typeName}";

            try
            {
                return new Uri(
                    $"rabbitmq://{_configuration["RabbitMQ:Host"]}{_configuration["RabbitMQ:VHost"]}/{_configuration[query]}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when fetching route on type {typeName} with route query '{query}'. {ex.Message}",ex);
                throw new GatewayException($"Unable to detect route for {typeName}",ex);
            }
        }
    }
}