using Heimdal.Transport.Interfaces;

namespace Heimdall.Contracts.Events
{
    /// <summary>
    /// Indicates a 
    /// </summary>
    public interface INewSightingReported : IUserCorrelated, IEvent
    {
        string Summary { get; }
    }
}