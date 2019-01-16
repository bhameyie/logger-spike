using System.Threading;
using System.Threading.Tasks;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public interface ISightingService
    {
        Task ReportSighting(SightingRequest validatedRequest, CancellationToken cancellationToken);
    }
}