using System;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Heimdall.Transport.RabbitMQ
{
    public class MqHostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly ILog _logger;

        public MqHostedService(IBusControl busControl)
        {
            _logger = LogManager.GetLogger(typeof(MqHostedService));
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (new StateLogScope(_logger, "Firing up the bus"))
            {
                await _busControl.StartAsync(cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using (new StateLogScope(_logger, "Stopping up the bus"))
            {
                await _busControl.StopAsync(TimeSpan.FromSeconds(10));
            }
        }
    }
}