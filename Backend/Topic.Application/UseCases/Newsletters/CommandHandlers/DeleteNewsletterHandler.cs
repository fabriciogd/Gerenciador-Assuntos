using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Contracts.Context;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.CommandHandlers;

/// <summary>
/// Handles the delete of a newsletter by processing the <see cref="DeleteNewsletterHandler"/> command.
/// Implements <see cref="IRequestHandler{DeleteNewsetter, CommandResult<NewsletterResponse>}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class DeleteNewsletterHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<DeleteNewsetter, CommandResult<bool>>
{
    public async Task<CommandResult<bool>> Handle(DeleteNewsetter request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting delete newsletter with data {@Request}", request);

        var newsletter = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (newsletter is null)
        {
            _logger.LogError("Newsletter with {Id} not found", request.Id);

            return new NotFoundException("Assunto não encontrado");
        }

        _repository.Remove(newsletter);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Newsletter deleted with success {@Newsletter}", newsletter);

        return true;
    }
}
