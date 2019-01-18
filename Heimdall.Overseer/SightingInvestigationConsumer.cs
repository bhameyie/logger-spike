using System.Linq;
using System.Threading.Tasks;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;
using Heimdall.Overseer.Analyzers;
using Heimdall.Transport.Interfaces;
using MassTransit;

namespace Heimdall.Overseer
{
    /// <summary>
    /// Investigates reported sighting and determines if closer examination is warranted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SightingInvestigationConsumer<T> : IConsumer<NewSightingReported> where T : IReportedSighting,new()
    {
        private readonly ISightingRepository<T> _sightingRepository;
        private readonly IProtocolTranslator _protocolTranslator;
        private readonly ISightingAnalyzerRepertoire _analyzerRepertoire;
        private readonly IRouteRegistry _routeRegistry;

        public SightingInvestigationConsumer(
            ISightingRepository<T> sightingRepository,
            IProtocolTranslator protocolTranslator, 
            ISightingAnalyzerRepertoire analyzerRepertoire,
            IRouteRegistry routeRegistry)
        {
            _sightingRepository = sightingRepository;
            _protocolTranslator= protocolTranslator;
            _analyzerRepertoire = analyzerRepertoire;
            _routeRegistry = routeRegistry;
        }

        public async Task Consume(ConsumeContext<NewSightingReported> context)
        {
            var reportedAnalysis = _analyzerRepertoire.All()
                .Select(e=> e.Analyze(context.Message,_sightingRepository))
                .ToArray();

            var sightingRecord = _protocolTranslator.TranslateToRecord<T>(context.Message, reportedAnalysis);
            _sightingRepository.Add(sightingRecord);

            var suspiciousResults = reportedAnalysis.Where(e => e.IsSuspicious).ToArray();
            if (suspiciousResults.Any())
            {
                InvestigateSighting investigateCommand = _protocolTranslator.Translate(context.Message, suspiciousResults);
                await context.Send(_routeRegistry.For<InvestigateSighting>(), investigateCommand);
            }
        }
    }

}