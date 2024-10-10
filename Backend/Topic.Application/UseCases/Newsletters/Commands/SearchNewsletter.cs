using MediatR;
using Topic.Application.Primitives;

namespace Topic.Application.UseCases.Newsletters.Commands;

public sealed record SearchNewsletter(Guid Id) : IRequest<CommandResult<bool>>;