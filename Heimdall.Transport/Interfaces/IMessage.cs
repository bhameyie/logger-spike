﻿using System;

namespace Heimdall.Transport.Interfaces
{
    /// <summary>
    /// Represents any message to be used in the system
    /// </summary>
    public interface IMessage
    {
        Guid CorrelationId { get; }
        DateTime Timestamp { get; }
    }
}
