using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Enums;

namespace Topic.Application.UseCases.Newsletters.Commands;

/// <summary>
/// Represents a command to create a new newsletter.
/// </summary>
/// <param name="Title">The title of the newsletter.</param>
/// <param name="Status">The status of the newsletter.</param>
/// <param name="Keywords">The keywords of the newsletter.</param>
public sealed record CreateNewsetter(
    string Title,
    StatusEnum Status,
    string[] Keywords): IRequest<CommandResult<NewsletterResponse>>;
