using Topic.Domain.Enums;

namespace Topic.Application.UseCases.Dashboards.Responses;

public sealed record DashboardResponse(StatusEnum status, int Count);