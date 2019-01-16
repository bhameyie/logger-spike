using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Heimdall.Contracts.Commands;
using Heimdall.Ingress.Models;
using NUnit;
using NUnit.Framework;

namespace Heimdall.Ingress.Tests
{
    public class RequestValidatorTests
    {
        private RequestValidator _sut;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _sut = new RequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void Should_Reject_Null_Request()
        {
            var validationResult = _sut.Validate(null);
            validationResult.Should().BeEquivalentTo(new ValidationResult("No request found."));
        }

        [TestCase("")]
        [TestCase("     ")]
        [TestCase(" ")]
        [TestCase((string)null)]
        public void Should_Reject_Empty_Origin(string origin)
        {
            var req = _fixture.Create<SightingRequest>();
            req.Origin = origin;

            var validationResult = _sut.Validate(req);

            validationResult.Succeeded.Should().BeFalse();
            validationResult.Reason.Should().Be("A sighting origin must be specified.");
        }

        [TestCase("")]
        [TestCase("     ")]
        [TestCase(" ")]
        [TestCase((string)null)]
        public void Should_Reject_Empty_CorrelationId(string correlationId)
        {
            var req = _fixture.Create<SightingRequest>();
            req.CorellationId = correlationId;

            var validationResult = _sut.Validate(req);

            validationResult.Succeeded.Should().BeFalse();
            validationResult.Reason.Should().Be("A sighting correlationId must be specified.");
        }

        [TestCase("")]
        [TestCase("     ")]
        [TestCase(" ")]
        [TestCase((string)null)]
        public void Should_Reject_Empty_Summary(string summary)
        {
            var req = _fixture.Create<SightingRequest>();
            req.Summary = summary;

            var validationResult = _sut.Validate(req);

            validationResult.Succeeded.Should().BeFalse();
            validationResult.Reason.Should().Be("A sighting summary must be specified.");
        }

        [Test]
        public void Should_Accept_PartiallyFilled_Request()
        {
            var req = _fixture.Create<SightingRequest>();
            req.SupplementalData = new Dictionary<string, object>();
            req.IncludedTrace = null;
            req.ReportedCause = null;

            _sut.Validate(req).Should().BeEquivalentTo(ValidationResult.Success);
        }

        [Test]
        public void Should_Accept_Full_Request()
        {
            var req = _fixture.Create<SightingRequest>();
            _sut.Validate(req).Should().BeEquivalentTo(ValidationResult.Success);
        }
    }
}
