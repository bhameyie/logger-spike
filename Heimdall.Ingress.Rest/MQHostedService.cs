using System;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MassTransit;

namespace Heimdall.Ingress
{
    public class MqHostedService : Microsoft.Extensions.Hosting.IHostedService
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