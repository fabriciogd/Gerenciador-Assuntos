namespace Topic.Api.Models;

/// <summary>
/// The <see cref="ApiResponseError"/> class represents an error message returned by the API. 
/// It contains a single message that provides details about the error.
/// </summary>
/// <param name="message">The error message to be returned in the API response.</param>
public class ApiResponseError(string message)
{
    /// <summary>
    /// Gets the error message returned by the API.
    /// </summary>
    public string Message { get; private set; } = message;
}