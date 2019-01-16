using System;
using System.Collections.Generic;
using System.Text;

namespace Heimdall.Contracts.Commands
{
    /// <summary>
    /// Commands the Overseer to investigate a reported issue
    /// </summary>
    public interface InvestigateSighting : IUserCorrelated
    {
        string Summary { get; }
        string Origin { get; }
        string ReportedCause { get; }
        string IncludedTrace { get; }
        Dictionary<string, object> SupplementalData { get; }
    }
}
