using System.Collections.Generic;
using Heimdall.DataAccess.Entities;

namespace Heimdall.DataAccess
{
    /// <summary>
    /// Sighting repository designed for less privileged access
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadonlySightingRepository<T> where T : IReportedSighting
    {
        IEnumerable<T> FindByReportedCorrelationId(string reportedCorrelationId);
    }
}