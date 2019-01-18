using Heimdall.Transport.Interfaces;

namespace Heimdall.Contracts
{
    /// <summary>
    /// Indicates what the user believes the issue is linked to
    /// </summary>
    public interface IUserCorrelated:IMessage
    {
        string ReportedCorellationId { get; }
    }
}