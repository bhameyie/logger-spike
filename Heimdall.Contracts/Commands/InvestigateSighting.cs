using System;
using System.Collections.Generic;

namespace Heimdall.Contracts.Commands
{
    /// <inheritdoc />
    public class InvestigateSighting : IInvestigateSighting
    {
        public Guid CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ReportedCorrelationId { get; set; }
        public string Summary { get; set; }
        public string Origin { get; set; }
        public string ReportedCause { get; set; }
        public string IncludedTrace { get; set; }
        public Dictionary<string, object> SupplementalData { get; set; }
        public IEnumerable<IAnalysisResults> SuspiciousResults { get; set; }

        public InvestigateSighting()
        {
            this.SupplementalData = new Dictionary<string, object>();
            this.SuspiciousResults = new IAnalysisResults[0];
        }
    }
}