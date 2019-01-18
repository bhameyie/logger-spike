using System;
using Heimdall.Contracts;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;

namespace Heimdall.Overseer
{
    public class CommandCreator : ICommandCreator
    {
        
        public InvestigateSighting Create(NewSightingReported request,
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
        }    }
}