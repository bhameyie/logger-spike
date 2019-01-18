using System.Threading.Tasks;
using Heimdall.Contracts.Commands;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;
using MassTransit;

namespace Heimdall.Overseer
{
    public class SightingInvestigator<T> : IConsumer<InvestigateSighting> where T : IReportedSighting
    {
        public SightingInvestigator(ISightingRepository<T> sightingRepository)
        {
        }

        public Task Consume(ConsumeContext<InvestigateSighting> context)
        {
            throw new System.NotImplementedException();
        }
    }
}