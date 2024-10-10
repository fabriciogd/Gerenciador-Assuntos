using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Contracts.Bus;
using Topic.Application.Contracts.Context;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Application.UseCases.Newsletters.IntegrationEvents;
using Topic.Domain.Enums;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.CommandHandlers;

/// <summary>
/// Handles the search of a newsletter by processing the <see cref="SearchNewsletterHandler"/> command.
/// Implements <see cref="IRequestHandler{DeleteNewsetter, CommandResult<NewsletterResponse>}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class SearchNewsletterHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository,
    IUnitOfWork _unitOfWork,
    IEventPublisher _eventPublisher) : IRequestHandler<SearchNewsletter, CommandResult<bool>>
{
    public async Task<CommandResult<bool>> Handle(SearchNewsletter request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting search newsletter with data {@Request}", request);

        var newsletter = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (newsletter is null)
        {
            _logger.LogError("Newsletter with {Id} not found", request.Id);

            return new NotFoundException("Assunto não encontrado");
        }

        newsletter.UpdateStatus(StatusEnum.InProgress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Newsletter {Id} search started with success", request.Id);

        _eventPublisher.Publish(new NewsletterSearchIntegrationEvent(newsletter.Id, "Add"));

        return true;
    }
}
