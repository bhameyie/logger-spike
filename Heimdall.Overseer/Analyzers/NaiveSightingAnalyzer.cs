using Heimdall.Contracts;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;

namespace Heimdall.Overseer.Analyzers
{
    public class NaiveSightingAnalyzer:ISightingAnalyzer{
        public IAnalysisResults Analyze<T>(NewSightingReported sighting, IReadonlySightingRepository<T> sightingRepository) where T : IReportedSighting
        {
            throw new System.NotImplementedException();
        }
    }
}