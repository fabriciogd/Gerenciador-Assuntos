using Microsoft.Extensions.Options;
using Topic.Infraestructure.News;

namespace Topic.Infraestructure.Handlers;

internal sealed class ApiKeyDelegatingHandler(IOptions<NewsSettings> _options) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("user-agent", "News-API-csharp/0.1");
        request.Headers.Add("x-api-key", _options.Value.ApiKey);

        return base.SendAsync(request, cancellationToken);
    }
}
