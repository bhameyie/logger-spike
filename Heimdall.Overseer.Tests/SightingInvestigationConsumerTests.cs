using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Heimdall.Contracts;
using Heimdall.Contracts.Commands;
using Heimdall.Contracts.Events;
using Heimdall.DataAccess;
using Heimdall.DataAccess.Entities;
using Heimdall.Overseer.Analyzers;
using Heimdall.Transport.Interfaces;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;

namespace Heimdall.Overseer.Tests
{
    [TestFixture]
    public class SightingInvestigationConsumerTests
    {
        private Mock<ISightingRepository<StubReportedSighting>> _repoMock;
        private Fixture _fixture;
        private Mock<IProtocolTranslator> _protoTranslator;
        private Mock<ISightingAnalyzerRepertoire> _repertoir;
        private Mock<IRouteRegistry> _routeRegistry;
        private InMemoryTestHarness _harness;
        private ConsumerTestHarness<SightingInvestigationConsumer<StubReportedSighting>> _consumer;

        [OneTimeSetUp]
        public async Task FixtureSetUp()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<ISightingRepository<StubReportedSighting>>();
            _protoTranslator = new Mock<IProtocolTranslator>();
            _repertoir = new Mock<ISightingAnalyzerRepertoire>();
            _routeRegistry = new Mock<IRouteRegistry>();
            _harness = new InMemoryTestHarness();
            _consumer = _harness.Consumer(() => new SightingInvestigationConsumer<StubReportedSighting>(
                _repoMock.Object, _protoTranslator.Object,
                _repertoir.Object, _routeRegistry.Object));

            var ev = _fixture.Create<NewSightingReported>();
            var chiefInvestigatorQ = new Uri("mq://chiefInspectorJapp");
            var dbRecord = new StubReportedSighting();
            var investigateCommand = new InvestigateSighting{Origin = "meh"};

            _repertoir.Setup(e => e.All()).Returns(new[] {new RiggedAnalyzer(true),new RiggedAnalyzer(false)  });

            _routeRegistry.Setup(e => e.For<InvestigateSighting>()).Returns(chiefInvestigatorQ);
            _protoTranslator.Setup(e => e.TranslateToRecord<StubReportedSighting>(It.IsAny<NewSightingReported>(), It.Is<IAnalysisResults[]>(p => p.Length == 2)))
                .Returns(dbRecord);
            _protoTranslator.Setup(e => e.Translate(It.IsAny<NewSightingReported>(), It.Is<IAnalysisResults[]>(p => p.All(x => x.IsSuspicious))))
                .Returns(investigateCommand);
            _repoMock.Setup(e => e.Add(dbRecord)).Returns(dbRecord);

            await _harness.Start();
            await _harness.InputQueueSendEndpoint.Send(ev);
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            await _harness.Stop();
        }

        [Test]
        public void Should_Command_Investigation_when_Suspicious_Analysis_Detected()
        {
            _harness.Sent.Select<InvestigateSighting>().Any().Should().BeTrue();
        }

        private class StubAnalysis:IAnalysisResults {
            public string Analyzer { get; set; }
            public Dictionary<string, object> Findings { get; set; }
            public bool IsSuspicious { get; set; }
        }

        public class StubReportedSighting : IReportedSighting
        {
        }

        private class RiggedAnalyzer : ISightingAnalyzer
        {
            private readonly bool _isSusispicious;

            public RiggedAnalyzer(bool isSusispicious)
            {
                _isSusispicious = isSusispicious;
            }

            public IAnalysisResults Analyze<T>(NewSightingReported sighting, IReadonlySightingRepository<T> sightingRepository) where T : IReportedSighting
            {
                return new StubAnalysis{IsSuspicious = _isSusispicious};
            }
        }
    }
}