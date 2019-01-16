using System;
using System.Collections.Generic;

namespace Heimdall.Contracts.Commands
{
    /// <inheritdoc />
    public class InvestigateSighting : IInvestigateSighting
    {
        public Guid CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ReportedCorellationId { get; set; }
        public string Summary { get; set; }
        public string Origin { get; set; }
        public string ReportedCause { get; set; }
        public string IncludedTrace { get; set; }
        public Dictionary<string, object> SupplementalData { get; set; }
    }
}