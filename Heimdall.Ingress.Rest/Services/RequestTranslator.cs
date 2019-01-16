using System;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public class RequestTranslator : IRequestTranslator
    {
        public IInvestigateSighting TranslateToInvestigationCommand(SightingRequest request, Guid correlationId)
        {
            return new InvestigateSighting
            {
                Summary = request.Summary,
                Origin = request.Origin,
                ReportedCause = request.ReportedCause,
                CorrelationId = Guid.NewGuid(),
                SupplementalData = request.SupplementalData,
                ReportedCorellationId = request.CorellationId,
                Timestamp = DateTime.UtcNow,
                IncludedTrace = request.IncludedTrace
            };
        }

        public NewSightingReported TranslateToNewSightingEvent(SightingRequest request, Guid correlationId)
        {
            return new NewSightingReported
            {
                Summary = request.Summary,
                CorrelationId = Guid.NewGuid(),
                ReportedCorellationId = request.CorellationId,
                Timestamp = DateTime.UtcNow,

            };
        }
    }
}