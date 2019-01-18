using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Heimdall.Ingress.Controllers;
using Heimdall.Ingress.Models;
using Heimdall.Ingress.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Heimdall.Ingress.Tests.Controllers
{
    public class SightingControllerTests
    {
        private SightingController _sut;
        private Fixture _fixture;
        private Mock<ISightingService> _service;
        private Mock<IRequestValidator> _requestValidator;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _requestValidator = new Mock<IRequestValidator>();
            _service = new Mock<ISightingService>();
            _sut = new SightingController(_requestValidator.Object, _service.Object);
        }

        [Test]
        public async Task Should_Not_Communicate_On_InvalidRequest()
        {
            var token = new CancellationToken();
            var req = _fixture.Create<SightingRequest>();
            _requestValidator.Setup(e => e.Validate(req)).Returns(new ValidationResult("meh"));

            var result = await _sut.Report(req, token);
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            _service.Verify(e => e.ReportSighting(req, token), Times.Never);
        }

        [Test]
        public async Task Should_Communicate_Out_When_RequestValid()
        {
            var token = new CancellationToken();
            var req = _fixture.Create<SightingRequest>();
            _requestValidator.Setup(e => e.Validate(req)).Returns(ValidationResult.Success);

            var result = await _sut.Report(req, token);
            result.Should().BeAssignableTo<OkResult>();

            _service.Verify(e => e.ReportSighting(req, token));
        }
    }
}