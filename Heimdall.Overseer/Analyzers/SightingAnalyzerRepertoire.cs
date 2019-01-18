using System.Collections.Generic;

namespace Heimdall.Overseer.Analyzers
{
    public class SightingAnalyzerRepertoire : ISightingAnalyzerRepertoire
    {
        public IEnumerable<ISightingAnalyzer> All()
        {
            //todo: User MEF
            throw new System.NotImplementedException();
        }
    }
}