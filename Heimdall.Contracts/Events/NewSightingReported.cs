using System;
using System.Collections.Generic;
using System.Text;

namespace Heimdall.Contracts.Events
{
    /// <summary>
    /// Indicates a 
    /// </summary>
    public interface NewSightingReported:IUserCorrelated
    {
        string Summary { get; }
    }
}
