using System;
using System.Threading;
using System.Threading.Tasks;
using Heimdall.Ingress.Models;
using Heimdall.Ingress.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Heimdall.Ingress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightingController : ControllerBase
    {
        private readonly IRequestValidator _requestValidator;
        private readonly ISightingService _service;
        private readonly ILog _logger;

        public SightingController(IRequestValidator requestValidator, ISightingService service)
        {
            _requestValidator = requestValidator;
            _service = service;
            _logger = LogManager.GetLogger(typeof(SightingController));
        }

        [HttpPost]
        public async Task<IActionResult> Report([FromBody] SightingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validate = _requestValidator.Validate(request);
                if (validate.Succeeded)
                {
                    await _service.ReportSighting(request, cancellationToken);
                    return Ok();
                }

                return BadRequest(new { rejectionReason = validate.Reason });
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured while reporting a sighting. ${ex.Message}", ex);
                throw;
            }
        }
    }
}
