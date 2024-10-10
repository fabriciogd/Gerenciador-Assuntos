using System.Net.Http.Json;
using Topic.Application.Contracts.News;
using Topic.Application.Contracts.News.Response;

namespace Topic.Infraestructure.News;

internal class NewsService(IHttpClientFactory _httpClientFactory) : INewsService
{
    private const int PageSize = 5;

    public async Task<NewsResponse> SearchAsync(string[] keywords, int count, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("News");

        var keywordQuery = string.Join("+", keywords);

        int page = count / PageSize; 

        var response = await client
            .GetFromJsonAsync<NewsResponse>($"?q={Uri.EscapeDataString(keywordQuery)}&language=pt&pageSize={PageSize}&page={++page}");

        return response;
    }
}
