using System;
using System.Threading;
using System.Threading.Tasks;
using Heimdall.Ingress.Models;
using MassTransit;

namespace Heimdall.Ingress.Services
{
    public class SightingService : ISightingService
    {
        private readonly ISendEndpoint _sendEndpoint;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestTranslator _translator;

        public SightingService(ISendEndpoint sendEndpoint,
            IPublishEndpoint publishEndpoint,
            IRequestTranslator translator)
        {
            _sendEndpoint = sendEndpoint;
            _publishEndpoint = publishEndpoint;
            _translator = translator;
        }

        public async Task ReportSighting(SightingRequest validatedRequest, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();
            await _sendEndpoint.Send(_translator.TranslateToInvestigationCommand(validatedRequest, correlationId),
                cancellationToken);

            await _publishEndpoint.Publish(
                _translator.TranslateToNewSightingEvent(validatedRequest, correlationId),
                cancellationToken);
        }
    }
}