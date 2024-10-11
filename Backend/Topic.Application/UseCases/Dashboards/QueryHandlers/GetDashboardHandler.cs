using MediatR;
using Microsoft.Extensions.Logging;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Dashboards.Queries;
using Topic.Application.UseCases.Dashboards.Responses;
using Topic.Application.UseCases.Newsletters.CommandHandlers;
using Topic.Domain.Repositories;

namespace Topic.Application.UseCases.Dashboards.QueryHandlers;

/// <summary>
/// Handles the query to retrieve dashboard
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{CreateNewsletterHandler}"/> for logging information and errors.</param>
/// <param name="_repository">An instance of <see cref="INewsletterRepository"/> for data access.</param>
internal sealed class GetDashboardHandler(
    ILogger<CreateNewsletterHandler> _logger,
    INewsletterRepository _repository) : IRequestHandler<GetDashboard, QueryResult<List<DashboardResponse>>>
{
    public async Task<QueryResult<List<DashboardResponse>>> Handle(GetDashboard request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting get dashboard");

        var list = await _repository.GetGroups(cancellationToken);

        return list.Select(a => new DashboardResponse(a.Key, a.Value)).ToList();
    }
}
