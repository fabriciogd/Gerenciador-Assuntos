using Microsoft.AspNetCore.Mvc;
using Topic.Api.Models;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;

namespace Topic.Api.Extensions;

public static class ResultOrErrorExtensions
{
    public static IActionResult MatchToResult<TResult>(this ResultOrError<TResult, Exception> result,
            Func<TResult, IActionResult> successAction = null!)
    {
        {
            successAction ??= (r) => new OkObjectResult(r);

            return result.Match(
                success => successAction(success),
                failure => ToHttpNonSuccessResult(failure));
        }
    }

    private static IActionResult ToHttpNonSuccessResult(Exception failure)
    {
        switch (failure)
        {
            case ValidationException ex:

                var validationErrors = ex
                    .Errors
                    .Select(validation => new ApiResponseError(validation.ErrorMessage))
                    .ToList();

                return new BadRequestObjectResult(ApiResponse.WithErrors(validationErrors));

            case AlreadyExistsException ex:
                return new ConflictObjectResult(ApiResponse.WithMessage(ex.Message));

            default:
                return new BadRequestObjectResult(ApiResponse.WithMessage("Ocorreu um erro"));
        }
    }
}