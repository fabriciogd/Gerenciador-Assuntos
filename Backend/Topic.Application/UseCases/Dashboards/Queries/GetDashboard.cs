using MediatR;
using Topic.Application.Primitives;
using Topic.Application.UseCases.Dashboards.Responses;

namespace Topic.Application.UseCases.Dashboards.Queries;

/// <summary>
/// Query dashboard
/// </summary>
public sealed record GetDashboard : IRequest<QueryResult<List<DashboardResponse>>>;
