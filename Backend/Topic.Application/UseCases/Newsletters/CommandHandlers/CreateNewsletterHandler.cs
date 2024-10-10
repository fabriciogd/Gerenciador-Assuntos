using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Contracts.Bus;
using Topic.Application.Contracts.Context;
using Topic.Application.Exceptions;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Application.UseCases.Newsletters.IntegrationEvents;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Entities;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Newsletters.CommandHandlers;

/// <summary>
/// Handles the creation of a new newsletter by processing the <see cref="CreateNewsletterHandler"/> command.
/// Implements <see cref="IRequestHandler{CreateNewsetter, CommandResult<NewsletterResponse>}"/> to manage request handling in a CQRS pattern.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
/// <param name="_unitOfWork">An instance of <see cref="IUnitOfWork"/> for managing database transactions.</param>
internal sealed class CreateNewsletterHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateNewsetter, CommandResult<NewsletterResponse>>
{
    public async Task<CommandResult<NewsletterResponse>> Handle(CreateNewsetter request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting create newsletter with data {@Request}", request);

        var newsletter = Newsletter.Create(
            request.Title,
            request.Status,
            request.Keywords
        );

        if (!newsletter.IsValid)
        {
            _logger.LogError("Create newsletter validated with errors {@Errors}", newsletter.Errors);

            return new ValidationException(newsletter.Errors);
        }

        if (await _repository.ExistsByTitleAsync(request.Title, cancellationToken))
        {
            _logger.LogError("Create newsletter with title {Title} duplicated error", request.Title);

            return new AlreadyExistsException("Titulo já está sendo usado por outro assunto");
        }

        await _repository.AddAsync(newsletter, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Newsletter created with success {@Newsletter}", newsletter);

        return new NewsletterResponse(
            newsletter.Id, 
            newsletter.Title, 
            newsletter.Status, 
            newsletter.Keywords);
    }
}
