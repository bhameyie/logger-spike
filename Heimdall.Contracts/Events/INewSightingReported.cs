namespace Heimdall.Contracts.Events
{
    /// <summary>
    /// Indicates a 
    /// </summary>
    public interface INewSightingReported : IUserCorrelated
    {
        string Summary { get; }
    }
}