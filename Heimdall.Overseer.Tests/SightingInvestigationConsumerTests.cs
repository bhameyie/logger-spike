using AutoFixture;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;
using Heimdall.Overseer.Analyzers;
using Moq;
using NUnit.Framework;

namespace Heimdall.Overseer.Tests
{
    public class SightingInvestigationConsumerTests
    {
        private Mock<ISightingRepository<StubReportedSighting>> _repoMock;
        private SightingInvestigationConsumer<StubReportedSighting> _sut;
        private Fixture _fixture;
        private Mock<ICommandCreator> _commandCreator;
        private Mock<ISightingAnalyzerRepertoire> _repertoir;

        [SetUp]
        public void Init()
        {
            _repoMock = new Mock<ISightingRepository<StubReportedSighting>>();
            _commandCreator = new Mock<ICommandCreator>();
            _repertoir = new Mock<ISightingAnalyzerRepertoire>();
            _sut = new SightingInvestigationConsumer<StubReportedSighting>(
                _repoMock.Object,
                _commandCreator.Object, _repertoir.Object);
            _fixture = new Fixture();
        }


        private class StubReportedSighting : IReportedSighting
        {
        }
    }
}