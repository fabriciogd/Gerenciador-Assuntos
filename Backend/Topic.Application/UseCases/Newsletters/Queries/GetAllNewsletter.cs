using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Responses;

namespace Topic.Application.UseCases.Newsletters.Queries;

/// <summary>
/// Query to retrieve all newsletter
public sealed record GetAllNewsletter : IRequest<QueryResult<List<NewsletterResponse>>>;