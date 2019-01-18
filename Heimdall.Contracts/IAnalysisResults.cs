using System.Collections.Generic;

namespace Heimdall.Contracts
{
    public interface IAnalysisResults
    {
        string Analyzer { get; }
        Dictionary<string, object> Findings { get; }
        bool IsSuspicious { get; }
    }
}