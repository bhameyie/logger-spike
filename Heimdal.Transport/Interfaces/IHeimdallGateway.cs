using System.Threading;
using System.Threading.Tasks;

namespace Heimdal.Transport.Interfaces
{
    public interface IHeimdallGateway
    {
        Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : IEvent;
        Task Send<T>(T message, CancellationToken cancellationToken = default(CancellationToken)) where T : ICommand;
    }
}
