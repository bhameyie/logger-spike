using System.Collections.Generic;

namespace Heimdall.Overseer.Analyzers
{
    public interface ISightingAnalyzerRepertoire
    {
        IEnumerable<ISightingAnalyzer> All();
    }
}