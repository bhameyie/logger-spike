using System.Collections.Generic;
using Heimdall.DataAccess.Entities;

namespace Heimdall.DataAccess
{
    public interface ISightingRepository<T> where T:IReportedSighting
    {
        T Add(T sighting) ;
        IEnumerable<T> FindByReportedCorrelationId(string reportedCorrelationId);
    }
}