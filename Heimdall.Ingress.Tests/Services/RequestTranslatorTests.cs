using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Heimdall.Ingress.Models;
using Heimdall.Ingress.Services;
using NUnit.Framework;

namespace Heimdall.Ingress.Tests.Services
{
    public class RequestTranslatorTests
    {
        private RequestTranslator _sut;
        private Fixture _fixture;
        private Guid _guid;

        [SetUp]
        public void SetUp()
        {
            _sut = new RequestTranslator();
            _fixture = new Fixture();
            _guid = Guid.NewGuid();
        }

        [Test]
        public void Should_Translate_PartiallyFilled_Request_ToCommand()
        {
            var req = _fixture.Create<SightingRequest>();
            req.SupplementalData = new Dictionary<string, object>();
            req.IncludedTrace = null;
            req.ReportedCause = null;

            var now = DateTime.UtcNow;
            var command = _sut.TranslateToInvestigationCommand(req,_guid );
            command.Summary.Should().Be(req.Summary);
            command.Origin.Should().Be(req.Origin);
            command.ReportedCorellationId.Should().Be(req.CorellationId);
            command.CorrelationId.Should().NotBe(Guid.Empty);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Should_Translate_Full_Request_ToCommand()
        {
            var req = _fixture.Create<SightingRequest>();

            var now = DateTime.UtcNow;
            var command = _sut.TranslateToInvestigationCommand(req, _guid);
            command.Summary.Should().Be(req.Summary);
            command.Origin.Should().Be(req.Origin);
            command.ReportedCorellationId.Should().Be(req.CorellationId);
            command.CorrelationId.Should().NotBe(Guid.Empty);
            command.IncludedTrace.Should().Be(req.IncludedTrace);
            command.ReportedCause.Should().Be(req.ReportedCause);
            command.SupplementalData.Should().BeEquivalentTo(req.SupplementalData);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }


        [Test]
        public void Should_Translate_PartiallyFilled_Request_ToEvent()
        {
            var req = _fixture.Create<SightingRequest>();
            req.SupplementalData = new Dictionary<string, object>();
            req.IncludedTrace = null;
            req.ReportedCause = null;

            var now = DateTime.UtcNow;
            var command = _sut.TranslateToNewSightingEvent(req, _guid);
            command.Summary.Should().Be(req.Summary);
            command.ReportedCorellationId.Should().Be(req.CorellationId);
            command.CorrelationId.Should().NotBe(Guid.Empty);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Should_Translate_Full_Request_ToEvent()
        {
            var req = _fixture.Create<SightingRequest>();

            var now = DateTime.UtcNow;
            var command = _sut.TranslateToNewSightingEvent(req, _guid);
            command.Summary.Should().Be(req.Summary);
            command.ReportedCorellationId.Should().Be(req.CorellationId);
            command.CorrelationId.Should().NotBe(Guid.Empty);
            (command.Timestamp - now).Should().BeLessThan(TimeSpan.FromSeconds(5));
        }
    }
}
