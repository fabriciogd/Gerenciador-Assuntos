using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Responses;

namespace Topic.Application.UseCases.Newsletters.Queries;

/// <summary>
/// Query to retrieve links by newsletter unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the newsletter.</param>
public sealed record GetLinksByNewsletterById(Guid Id) : IRequest<QueryResult<List<NewsletterLinkResponse>>>;