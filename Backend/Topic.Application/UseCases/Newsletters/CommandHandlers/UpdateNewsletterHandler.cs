using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Contracts.Bus;
using Topic.Application.Contracts.Context;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Application.UseCases.Newsletters.IntegrationEvents;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Enums;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.CommandHandlers;

/// <summary>
/// Handles the update of a newsletter by processing the <see cref="UpdateNewsletterHandler"/> command.
/// Implements <see cref="IRequestHandler{UpdateNewsetter, CommandResult<NewsletterResponse>}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class UpdateNewsletterHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository,
    IUnitOfWork _unitOfWork,
    IEventPublisher _eventPublisher) : IRequestHandler<UpdateNewsetter, CommandResult<NewsletterResponse>>
{
    public async Task<CommandResult<NewsletterResponse>> Handle(UpdateNewsetter request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting update newsletter with data {@Request}", request);

        var newsletter = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (newsletter is null)
        {
            _logger.LogError("Newsletter with {Id} not found", request.Id);

            return new NotFoundException("Assunto não encontrado");
        }

        newsletter.Update(request.Title, request.Status, request.Keywords);

        if (!newsletter.IsValid)
        {
            _logger.LogError("Update newsletter validated with errors {@Errors}", newsletter.Errors);

            return new ValidationException(newsletter.Errors);
        }

        _repository.Update(newsletter);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Newsletter updated with success {@Newsletter}", newsletter);

        _eventPublisher.Publish(new NewsletterSearchIntegrationEvent(
            newsletter.Id, 
            newsletter.Status == StatusEnum.InProgress ? 
                "Add" : "Remove"
        ));

        return new NewsletterResponse(
          newsletter.Id,
          newsletter.Title,
          newsletter.Status,
          newsletter.Keywords);
    }
}
