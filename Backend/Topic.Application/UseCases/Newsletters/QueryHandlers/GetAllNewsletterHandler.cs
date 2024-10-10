using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.CommandHandlers;
using Topic.Application.UseCases.Newsletters.Queries;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.QueryHandlers;

/// <summary>
/// Handles the query to retrieve al newsletter.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
internal sealed class GetAllNewsletterHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository) : IRequestHandler<GetAllNewsletter, QueryResult<List<NewsletterResponse>>>
{
    public async Task<QueryResult<List<NewsletterResponse>>> Handle(GetAllNewsletter request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get all newsletter");

        var list = await _repository.GetAllAsync(cancellationToken);

        _logger.LogInformation("Founded {Count} newsletter", list.Count);

        return list.Select(a =>
            new NewsletterResponse(
                a.Id,
                a.Title,
                a.Status,
                a.Keywords,
                a.Links.Select(b => 
                    new NewsletterLinkResponse(
                        b.Id, 
                        b.Title, 
                        b.Description, 
                        b.Url
                    )
                )
            )
        ).ToList();
    }
}
