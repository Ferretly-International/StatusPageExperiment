using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

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
    private readonly IConfiguration _configuration;

    public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient();
        
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _configuration["StatusPage:ApiKey"]);
        client.BaseAddress = new Uri(_configuration["StatusPage:ApiUrl"] ?? 
                                     throw new InvalidOperationException("StatusPage:ApiUrl is not set"));
        
        return client;
    }
}