using MediatR;
using Topic.Application.Primitives;

namespace Topic.Application.UseCases.Newsletters.Commands;

/// <summary>
/// Represents a command to delete a newsletter.
/// </summary>
/// <param name="Id">The unique identifier newsletter.</param>
public sealed record DeleteNewsetter(Guid Id) : IRequest<CommandResult<bool>>;