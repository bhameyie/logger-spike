using Heimdall.Ingress.Models;
using Heimdall.Ingress.Services;

namespace Heimdall.Ingress
{
    public class RequestValidator : IRequestValidator
    {
        public ValidationResult Validate(SightingRequest request)
        {
            ValidationResult MissingFieldResult(string field)
            {
                return new ValidationResult($"A sighting {field} must be specified.");
            }

            if (request == null)
            {
                return new ValidationResult("No request found.");
            }

            if (string.IsNullOrWhiteSpace(request.Summary))
            {
                return MissingFieldResult("summary");
            }

            if (string.IsNullOrWhiteSpace(request.Origin))
            {
                return MissingFieldResult("origin");
            }

            if (string.IsNullOrWhiteSpace(request.CorellationId))
            {
                return MissingFieldResult("correlationId");
            }

            return ValidationResult.Success;
        }
    }
}
