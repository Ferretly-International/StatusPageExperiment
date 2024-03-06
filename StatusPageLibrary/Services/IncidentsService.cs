using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StatusPageLibrary.Models;

namespace StatusPageLibrary.Services;

public interface IIncidentsService
{
    /// <summary>
    /// Fetch a list of active incidents
    /// </summary>
    /// <returns></returns>
    Task<List<Incident>> GetActiveIncidentsAsync();

    /// <summary>
    /// Get all incidents, optionally filtered by query, limit, and page
    /// </summary>
    /// <param name="query">If specified, searches for the text query string in the incidents' name, status, postmortem_body, and incident_updates fields.</param>
    /// <param name="limit">The maximum number of rows to return per page. The default and maximum limit is 100.</param>
    /// <param name="page">Page offset to fetch</param>
    /// <returns></returns>
    Task<List<Incident>> GetIncidentHistoryAsync(string? query = null, int limit = 100, int page = 1);
}

/// <summary>
/// Service used for querying, adding, and acting on incidents
/// </summary>
public class IncidentsService: IIncidentsService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public IncidentsService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task<List<Incident>> GetActiveIncidentsAsync()
    {
        using var client = _httpClientService.GetClient();
        var result = await client
            .GetAsync($"pages/{_configuration["StatusPage:PageId"]}/incidents/unresolved");
        
        if (!result.IsSuccessStatusCode) throw new HttpRequestException(message: "Failed to retrieve active incidents",
            null,
            statusCode: result.StatusCode);
        
        var content = await result.Content.ReadAsStringAsync();
        
        var incidents = JsonSerializer.Deserialize<List<Incident>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return incidents ?? new List<Incident>();
    }

    public async Task<List<Incident>> GetIncidentHistoryAsync(string? query = null, int limit = 100, int page = 1)
    {
        var url = $"pages/{_configuration["StatusPage:PageId"]}/incidents?limit={limit}&page={page}";
        if (!string.IsNullOrWhiteSpace(query))
        {
            url += $"&q={UrlEncoder.Create().Encode(query)}";
        }

        using var client = _httpClientService.GetClient();
        var result = await client.GetAsync(url);

        var content = await result.Content.ReadAsStringAsync();
        var incidents = JsonSerializer.Deserialize<List<Incident>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        return incidents ?? new List<Incident>();
    }
}