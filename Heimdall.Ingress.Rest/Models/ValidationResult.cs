namespace Heimdall.Ingress.Models
{
    public class ValidationResult
    {
        public string Reason { get; }
        public bool Succeeded { get; }
        public static ValidationResult Success { get; } = new ValidationResult();

        public ValidationResult(string reason)
        {
            Reason = reason;
            Succeeded = false;
        }

        private ValidationResult()
        {
            Succeeded = true;
        }

    }
}