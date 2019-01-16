using System.Collections.Generic;

namespace Heimdall.Ingress.Models
{
    public class SightingRequest
    {
        public string CorellationId { get; set; }
        public string Summary { get; set; }
        public string Origin { get; set; }
        public string ReportedCause { get; set; }
        public string IncludedTrace { get; set; }
        public Dictionary<string, object> SupplementalData { get; set; }

        public SightingRequest()
        {
            SupplementalData = new Dictionary<string, object>();
        }
    }
}
