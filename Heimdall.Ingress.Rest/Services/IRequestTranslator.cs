using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public interface IRequestTranslator
    {
        InvestigateSighting TranslateToInvestigationCommand(SightingRequest request);
        NewSightingReported TranslateToNewSightingEvent(SightingRequest request);
    }
}