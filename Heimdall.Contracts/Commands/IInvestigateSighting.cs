﻿using System.Collections.Generic;
using System.Text;
using Heimdall.Transport.Interfaces;

namespace Heimdall.Contracts.Commands
{
    /// <summary>
    /// Commands the Overseer to investigate a reported issue
    /// </summary>
    public interface IInvestigateSighting : IUserCorrelated, ICommand
    {
        string Summary { get; }
        string Origin { get; }
        string ReportedCause { get; }
        string IncludedTrace { get; }
        Dictionary<string, object> SupplementalData { get; }
        IEnumerable<IAnalysisResults> SuspiciousResults { get; }
    }
}
