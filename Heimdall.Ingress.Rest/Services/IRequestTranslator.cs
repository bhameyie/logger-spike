using System;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public interface IRequestTranslator
    {
        IInvestigateSighting TranslateToInvestigationCommand(SightingRequest request, Guid correlationId);
        NewSightingReported TranslateToNewSightingEvent(SightingRequest request, Guid correlationId);
    }
}