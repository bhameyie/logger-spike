using System;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public class RequestTranslator : IRequestTranslator
    {

        public NewSightingReported TranslateToNewSightingEvent(SightingRequest request, Guid correlationId)
        {
            return new NewSightingReported
            {
                Summary = request.Summary,
                Origin = request.Origin,
                ReportedCause = request.ReportedCause,
                CorrelationId = correlationId,
                SupplementalData = request.SupplementalData,
                ReportedCorrelationId = request.CorellationId,
                Timestamp = DateTime.UtcNow,
                IncludedTrace = request.IncludedTrace

            };
        }
    }
}