using System.Collections.Immutable;

namespace Topic.Api.Models;

/// <summary>
/// Represents API an error response.
/// </summary>
public class ApiResponse
{

    /// <summary>
    /// Gets the collection of errors returned when the operation fails.
    /// If the operation is successful, this will be an empty collection.
    /// </summary>
    public IReadOnlyCollection<ApiResponseError> Errors { get; protected init; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse"/> class.
    /// </summary>
    /// <param name="errors">The errors.</param>
    private ApiResponse(IReadOnlyCollection<ApiResponseError> errors)
    {
        Errors = errors;
    }

    public static ApiResponse WithErrors(List<ApiResponseError> validationErrors)
    {
        return new ApiResponse(validationErrors);
    }

    public static ApiResponse WithMessage(string message)
    {
        return new ApiResponse([new(message)]);
    }
}
