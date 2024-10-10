namespace Topic.Application.UseCases.Newsletters.Responses;

public sealed record NewsletterLinkResponse(Guid Id, string Title, string Description, string Url);
