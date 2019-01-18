using Heimdall.Contracts;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess.Entities;

namespace Heimdall.Overseer
{
    public interface IProtocolTranslator
    {
        InvestigateSighting Translate(NewSightingReported request,
            IAnalysisResults[] suspiciousResults);

        T TranslateToRecord<T>(NewSightingReported request, IAnalysisResults[] allResults)
            where T : IReportedSighting, new();
    }
}