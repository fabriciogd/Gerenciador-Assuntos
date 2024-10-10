using System.Text.Json;

namespace Topic.Api.Tests.Extensions;

internal static class HttpResponseMessageExtensions
{
    public async static Task<T> ParseTo<T>(this HttpResponseMessage response, JsonSerializerOptions options)
    {
        string reaponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(reaponse, options);
    }
}