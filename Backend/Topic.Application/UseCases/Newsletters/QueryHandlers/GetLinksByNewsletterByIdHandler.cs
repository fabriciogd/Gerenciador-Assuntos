using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Queries;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.QueryHandlers;

/// <summary>
/// Handles the query to retrieve al links.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{GetLinksByNewsletterById}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
public class GetLinksByNewsletterByIdHandler(
    ILogger<GetLinksByNewsletterById> _logger,
    INewsletterRepository _repository) : IRequestHandler<GetLinksByNewsletterById, QueryResult<List<NewsletterLinkResponse>>>
{
    public async Task<QueryResult<List<NewsletterLinkResponse>>> Handle(GetLinksByNewsletterById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get all newsletter");

        var list = await _repository.GetAllLinks(request.Id, cancellationToken);

        _logger.LogInformation("Founded {Count} links in newsletter {Id}", list.Count, request.Id);

        return list.Select(a =>
            new NewsletterLinkResponse(
                a.Id,
                a.Title,
                a.Description,
                a.Url
            )
        ).ToList();
    }
}
