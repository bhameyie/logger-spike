using System;
using log4net;

namespace Heimdal.Transport
{
    public class StateLogScope : IDisposable
    {
        private readonly ILog _logger;
        private readonly string _operation;

        public StateLogScope(ILog logger, string operation)
        {
            _logger = logger;
            _operation = operation;
            _logger.Info($"{operation}. State: Starting");
        }

        public void Dispose()
        {
            _logger.Info($"{_operation}. State: Completed");
        }
    }
}