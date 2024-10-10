namespace Topic.Application.Contracts.News.Response;

public sealed class NewsResponse
{
    public string Status { get; set; }

    public int TotalResults { get; set; }

    public IEnumerable<NewsArticleResponse> Articles { get; set; }
}
