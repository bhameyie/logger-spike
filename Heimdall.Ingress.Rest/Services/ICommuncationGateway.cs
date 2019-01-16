using System.Threading;
using System.Threading.Tasks;

namespace Heimdall.Ingress.Services
{
    public interface ICommunicationGateway
    {
        Task Publish<T>(T @event, CancellationToken cancellationToken);
        Task Send<T>(T command, CancellationToken cancellationToken);
    }
}
