using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Topic.Api.Tests.Extensions;
using Topic.Application.UseCases.Newsletters.Commands;
using Topic.Application.UseCases.Newsletters.Responses;
using Topic.Domain.Enums;

namespace Topic.Api.Tests.Controllers;

public class NewsletterControllerTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private JsonSerializerOptions _options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public NewsletterControllerTest(CustomWebApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_Success_When_Request_Correct()
    {
        CreateNewsetter newsetter = new("Title", StatusEnum.Pending, ["A", "B"]);

        var response = await _httpClient
            .PostAsJsonAsync("api/assuntos", newsetter, _options);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Error_When_Title_Is_Empty()
    {
        CreateNewsetter newsetter = new("", StatusEnum.Pending, ["A", "B"]);

        var response = await _httpClient
            .PostAsJsonAsync("api/assuntos", newsetter, _options);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Success_When_Get_By_Id()
    {
        CreateNewsetter newsetter = new("Title 2", StatusEnum.Pending, ["A", "B"]);

        var response = await _httpClient
            .PostAsJsonAsync("api/assuntos", newsetter);

        var entity = await response.ParseTo<NewsletterResponse>(_options);

        response = await _httpClient
           .GetAsync($"api/assuntos/{entity.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Error_When_Duplicated()
    {
        CreateNewsetter newsetter = new("Title 3", StatusEnum.Pending, ["A", "B"]);

        var response = await _httpClient
            .PostAsJsonAsync("api/assuntos", newsetter);

        response = await _httpClient
            .PostAsJsonAsync("api/assuntos", newsetter);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}