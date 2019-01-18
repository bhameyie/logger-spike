using Heimdall.DataAccess.Entities;

namespace Heimdall.DataAccess
{
    /// <summary>
    /// Full fledged sIghting repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISightingRepository<T>:IReadonlySightingRepository<T> where T:IReportedSighting
    {
        T Add(T sighting) ;
    }
}