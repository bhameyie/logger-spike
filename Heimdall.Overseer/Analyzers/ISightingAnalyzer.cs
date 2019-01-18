using Heimdall.Contracts;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;

namespace Heimdall.Overseer.Analyzers
{
    /// <summary>
    /// Analyzes sighting to determine if they are worth investigating
    /// </summary>
    public interface ISightingAnalyzer
    {
        /// <summary>
        /// Performs analysis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sighting"></param>
        /// <param name="sightingRepository">the sighting datastore</param>
        /// <returns></returns>
        IAnalysisResults Analyze<T>(NewSightingReported sighting, IReadonlySightingRepository<T> sightingRepository) where T : IReportedSighting;
    }
}