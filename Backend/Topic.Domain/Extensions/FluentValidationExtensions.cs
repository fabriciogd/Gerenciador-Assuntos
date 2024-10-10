using FluentValidation.Results;
using Topic.Domain.Primitives;

namespace Topic.Domain.Extensions;

internal static class FluentValidationExtensions
{
    public static List<ValidationError> AsErrors(this ValidationResult valResult)
    {
        var resultErrors = new List<ValidationError>();

        foreach (var valFailure in valResult.Errors)
        {
            resultErrors.Add(new ValidationError(
                valFailure.PropertyName,
                valFailure.ErrorMessage,
                valFailure.ErrorCode
            ));
        }

        return resultErrors;
    }
}
