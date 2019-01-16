using Heimdall.Ingress.Models;

namespace Heimdall.Ingress.Services
{
    public interface IRequestValidator
    {
        ValidationResult Validate(SightingRequest request);
    }
}
