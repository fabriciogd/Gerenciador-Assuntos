using System.Net.Http.Json;
using Topic.Application.Contracts.News;
using Topic.Application.Contracts.News.Response;

namespace Topic.Infraestructure.News;

internal class NewsService(IHttpClientFactory _httpClientFactory) : INewsService
{
    public async Task<NewsResponse> SearchAsync(string[] keywords, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("News");

        var keywordQuery = string.Join("+", keywords);

        var response = await client.GetFromJsonAsync<NewsResponse>($"?q={Uri.EscapeDataString(keywordQuery)}&language=pt&pageSize=20&page=1");

        return response;
    }
}
