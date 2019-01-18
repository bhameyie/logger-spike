using Heimdall.Contracts;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;

namespace Heimdall.Overseer
{
    public interface ICommandCreator
    {
        InvestigateSighting Create(NewSightingReported request,
            IAnalysisResults[] suspiciousResults);
    }
}