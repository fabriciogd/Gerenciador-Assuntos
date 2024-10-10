using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Enums;

namespace Topic.Application.UseCases.Newsletters.Commands;

/// <summary>
/// Represents a command to update newsletter.
/// </summary>
/// <param name="Id">The unique identifier newsletter.</param>
/// <param name="Title">The title of the newsletter.</param>
/// <param name="Status">The status of the newsletter.</param>
/// <param name="Keywords">The keywords of the newsletter.</param>
public sealed record UpdateNewsetter(
    Guid Id,
    string Title,
    StatusEnum Status,
    string[] Keywords) : IRequest<CommandResult<NewsletterResponse>>;