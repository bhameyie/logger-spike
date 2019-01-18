using System;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public interface IRequestTranslator
    {
        NewSightingReported TranslateToNewSightingEvent(SightingRequest request, Guid correlationId);
    }
}