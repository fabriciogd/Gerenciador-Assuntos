using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.CommandHandlers;
using Topic.Application.UseCases.Newsletters.Queries;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a newsletter by its unique identifier.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{CreateNewsletterHandler}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
internal sealed class GetNewsletterByIdHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository) : IRequestHandler<GetNewsletterById, QueryResult<NewsletterResponse>>
{
    public async Task<QueryResult<NewsletterResponse>> Handle(GetNewsletterById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get newsletter by id {Id}", request.Id);

        var newsletter = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (newsletter is null)
        {
            _logger.LogError("Newsletter with {Id} not found", request.Id);

            return new NotFoundException("Assunto não encontrado");
        }

        _logger.LogInformation("Newsletter founded {@Motorcycle}", newsletter);

        return new NewsletterResponse(newsletter.Id, newsletter.Title, newsletter.Status, newsletter.Keywords);
    }
}
