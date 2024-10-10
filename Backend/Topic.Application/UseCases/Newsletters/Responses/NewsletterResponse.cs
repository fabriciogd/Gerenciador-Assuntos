using Topic.Domain.Enums;

namespace Topic.Application.UseCases.Newsletters.Responses;

public sealed record NewsletterResponse(Guid Id, string Title, StatusEnum status, string[] Keywords, IEnumerable<NewsletterLinkResponse>? Links = default);
