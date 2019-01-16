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
        public string ReportedCorellationId { get; set; }
        public string Summary { get; set; }
    }
}
