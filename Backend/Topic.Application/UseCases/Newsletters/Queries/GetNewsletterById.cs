using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Responses;

namespace Topic.Application.UseCases.Newsletters.Queries;

/// <summary>
/// Query to retrieve a newsletter by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the newsletter to retrieve.</param>
public sealed record GetNewsletterById(Guid Id) : IRequest<QueryResult<NewsletterResponse>>;
