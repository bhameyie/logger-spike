using System;
using Heimdall.Contracts;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess.Entities;

namespace Heimdall.Overseer
{
    public class ProtocolTranslator : IProtocolTranslator
    {
       public InvestigateSighting Translate(NewSightingReported request,
            IAnalysisResults[] suspiciousResults)
        {
            return new InvestigateSighting
            {
                Summary = request.Summary,
                Origin = request.Origin,
                ReportedCause = request.ReportedCause,
                CorrelationId = request.CorrelationId,
                SupplementalData = request.SupplementalData,
                ReportedCorrelationId = request.ReportedCorrelationId,
                Timestamp = DateTime.UtcNow,
                IncludedTrace = request.IncludedTrace,
                SuspiciousResults=suspiciousResults
            };
        }

        public T TranslateToRecord<T>(NewSightingReported request, IAnalysisResults[] allResults) where T : IReportedSighting, new()
        {
            throw new NotImplementedException();
        }
    }
}