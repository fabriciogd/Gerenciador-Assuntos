using Topic.Domain.Enums;
using Topic.Domain.Extensions;

namespace Topic.Application.UseCases.Newsletters.Responses;

public class NewsletterResponse
{
    public NewsletterResponse(Guid id, string title, StatusEnum status, string[] keywords)
    {
        Id = id;
        Title = title;
        Status = status;
        Keywords = keywords;
    }

    public NewsletterResponse(Guid id, string title, StatusEnum status, string[] keywords, IEnumerable<NewsletterLinkResponse>? links)
    {
        Id = id;
        Title = title;
        Status = status;
        Keywords = keywords;
        Links = links;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public StatusEnum Status { get; private set; }
    public string StatusDescription => Status.GetDescription();
    public string[] Keywords { get; private set; }
    public IEnumerable<NewsletterLinkResponse>? Links { get; private set; }
}
