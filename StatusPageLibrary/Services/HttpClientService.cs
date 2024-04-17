using System.Net.Http.Headers;

namespace StatusPageLibrary.Services;

public interface IHttpClientService
{
    /// <summary>
    /// Construct and return a usable HttpClient
    /// </summary>
    /// <returns></returns>
    HttpClient GetClient();
}

public class HttpClientService: IHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IncidentsService.Configuration _configuration;

    public HttpClientService(
        IHttpClientFactory httpClientFactory, 
        IncidentsService.Configuration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient();
        
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(
                "Bearer", 
                _configuration.ApiKey);
        client.BaseAddress = new Uri(_configuration.ApiUrl ?? 
                                     throw new InvalidOperationException("StatusPage:ApiUrl is not set"));
        
        return client;
    }
}