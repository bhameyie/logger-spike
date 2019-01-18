using System.Threading.Tasks;
using Heimdall.Contracts.Commands;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;
using Heimdall.Overseer.Analyzers;
using MassTransit;

namespace Heimdall.Overseer
{
    /// <summary>
    /// Investigates reported sighting and determines if closer examination is warranted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SightingInvestigationConsumer<T> : IConsumer<InvestigateSighting> where T : IReportedSighting
    {
        private readonly ISightingRepository<T> _sightingRepository;
        private readonly ICommandCreator _commandCreatorObject;

        public SightingInvestigationConsumer(ISightingRepository<T> sightingRepository,
            ICommandCreator commandCreatorObject, ISightingAnalyzerRepertoire analyzerRepertoire)
        {
            _sightingRepository = sightingRepository;
            _commandCreatorObject = commandCreatorObject;
        }

        public Task Consume(ConsumeContext<InvestigateSighting> context)
        {

            throw new System.NotImplementedException();
        }
    }
}