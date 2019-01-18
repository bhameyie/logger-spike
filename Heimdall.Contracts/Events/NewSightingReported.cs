using System;
using System.Collections.Generic;
using System.Text;

namespace Heimdall.Contracts.Events
{
    /// <inheritdoc />
    public class NewSightingReported : INewSightingReported
    {
        public Guid CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ReportedCorrelationId { get; set; }
        public string Summary { get; set; }
        public string Origin { get; set; }
        public string ReportedCause { get; set; }
        public string IncludedTrace { get; set; }
        public Dictionary<string, object> SupplementalData { get; set; }
    }
}