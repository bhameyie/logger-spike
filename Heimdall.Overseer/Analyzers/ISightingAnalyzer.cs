using Heimdall.Contracts;
using Heimdall.Contracts.Events;

namespace Heimdall.Overseer.Analyzers
{
    public interface ISightingAnalyzer
    {
        IAnalysisResults Analyze(NewSightingReported sighting);
    }
}