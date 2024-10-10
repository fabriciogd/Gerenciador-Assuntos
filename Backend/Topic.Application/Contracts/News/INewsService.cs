using Topic.Application.Contracts.News.Response;

namespace Topic.Application.Contracts.News;

public interface INewsService
{
    Task<NewsResponse> SearchAsync(string[] keywords, CancellationToken cancellationToken);
}
