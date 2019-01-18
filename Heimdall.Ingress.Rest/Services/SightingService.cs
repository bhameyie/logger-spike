using System;
using System.Threading;
using System.Threading.Tasks;
using Heimdall.Ingress.Models;
using Heimdall.Transport.Interfaces;
using MassTransit;

namespace Heimdall.Ingress.Services
{
    public class SightingService : ISightingService
    {
        private readonly IRequestTranslator _translator;
        private readonly IHeimdallGateway _gateway;

        public SightingService(IRequestTranslator translator,IHeimdallGateway gateway)
        {
            _translator = translator;
            _gateway = gateway;
        }

        public async Task ReportSighting(SightingRequest validatedRequest, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();
            await _gateway.Send(_translator.TranslateToInvestigationCommand(validatedRequest, correlationId),
                cancellationToken);

            await _gateway.Publish(
                _translator.TranslateToNewSightingEvent(validatedRequest, correlationId),
                cancellationToken);
        }
    }
}