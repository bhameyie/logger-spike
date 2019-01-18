using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Heimdall.Contracts;
using Heimdall.Contracts.Events;
using NUnit.Framework;

namespace Heimdall.Overseer.Tests
{
    public class CommandCreatorTests
    {
        private Fixture _fixture;
        private CommandCreator _sut;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _sut = new CommandCreator();
        }

        [Test]
        public void Should_Translate_PartiallyFilled_Event_ToCommand()
        {
            var req = _fixture.Create<NewSightingReported>();
            req.SupplementalData = new Dictionary<string, object>();
            req.IncludedTrace = null;
            req.ReportedCause = null;

            var now = DateTime.UtcNow;
            var command = _sut.Create(req, new IAnalysisResults[0]);
            command.Summary.Should().Be(req.Summary);
            command.Origin.Should().Be(req.Origin);
            command.ReportedCorrelationId.Should().Be(req.ReportedCorrelationId);
            command.CorrelationId.Should().Be(req.CorrelationId);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Should_Translate_Full_Event_ToCommand()
        {
            var someRes = new List<IAnalysisResults> {new StubResults()};
            var req = _fixture.Create<NewSightingReported>();

            var now = DateTime.UtcNow;
            var command = _sut.Create(req, someRes.ToArray());
            command.Summary.Should().Be(req.Summary);
            command.Origin.Should().Be(req.Origin);
            command.ReportedCorrelationId.Should().Be(req.ReportedCorrelationId);
            command.CorrelationId.Should().Be(req.CorrelationId);
            command.IncludedTrace.Should().Be(req.IncludedTrace);
            command.ReportedCause.Should().Be(req.ReportedCause);
            command.SuspiciousResults.Single().Should().BeEquivalentTo(someRes.Single());
            command.SupplementalData.Should().BeEquivalentTo(req.SupplementalData);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        private class StubResults : IAnalysisResults
        {
            public string Analyzer
            {
                get { return "LOL"; }
            }

            public Dictionary<string, object> Findings { get; }
            public bool IsSuspicious { get; }
        }
    }
}