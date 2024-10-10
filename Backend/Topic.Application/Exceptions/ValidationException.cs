using Topic.Domain.Primitives;

namespace Topic.Application.Exceptions;

public sealed class ValidationException(List<ValidationError> errors, string message = "") : ApplicationException(message)
{
    public List<ValidationError> Errors = errors ?? throw new ArgumentNullException(nameof(errors));
}