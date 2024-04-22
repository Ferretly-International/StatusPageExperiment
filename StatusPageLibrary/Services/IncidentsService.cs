using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using StatusPageLibrary.Models;

namespace StatusPageLibrary.Services;

public interface IIncidentsService
{
    /// <summary>
    /// Fetch a list of active incidents as a <see cref="DTO.Incident"/>
    /// </summary>
    /// <returns></returns>
    Task<List<DTO.Incident>> GetActiveIncidentsAsDtoAsync();

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
    
    /// <summary>
    /// Update an existing incident
    /// </summary>
    /// <param name="incident"></param>
    /// <returns></returns>
    Task<HttpStatusCode> UpdateIncidentAsync(PatchIncident incident);
    
    /// <summary>
    /// Create a new incident
    /// </summary>
    /// <param name="incident"></param>
    /// <returns></returns>
    Task<Incident?> CreateIncidentAsync(PostIncident incident);
}

/// <summary>
/// Service used for querying, adding, and acting on incidents
/// </summary>
public class IncidentsService: IIncidentsService
{
    private readonly IHttpClientService _httpClientService;
    private readonly Configuration _configuration;

    public IncidentsService(
        IHttpClientService httpClientService, 
        Configuration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task<List<DTO.Incident>> GetActiveIncidentsAsDtoAsync()
    {
        var activeIncidents = await GetActiveIncidentsAsync();
        return activeIncidents.Select(i => i.AsDto()).ToList();
    }

    /// <inheritdoc />
    public async Task<List<Incident>> GetActiveIncidentsAsync()
    {
        using var client = _httpClientService.GetClient();
        var result = await client
            .GetAsync($"pages/{_configuration.PageId}/incidents/unresolved");
        
        if (!result.IsSuccessStatusCode) throw new HttpRequestException(message: "Failed to retrieve active incidents",
            null,
            statusCode: result.StatusCode);
        
        var content = await result.Content.ReadAsStringAsync();
        
        var incidents = JsonSerializer.Deserialize<List<Incident>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        return incidents ?? new List<Incident>();
    }

    public async Task<List<Incident>> GetIncidentHistoryAsync(string? query = null, int limit = 100, int page = 1)
    {
        var url = $"pages/{_configuration.PageId}/incidents?limit={limit}&page={page}";
        if (!string.IsNullOrWhiteSpace(query))
        {
            url += $"&q={UrlEncoder.Create().Encode(query)}";
        }

        using var client = _httpClientService.GetClient();
        var result = await client.GetAsync(url);

        var content = await result.Content.ReadAsStringAsync();
        var incidents = JsonSerializer.Deserialize<List<Incident>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        
        return incidents ?? new List<Incident>();
    }

    /// <inheritdoc />
    public async Task<HttpStatusCode> UpdateIncidentAsync(PatchIncident incident)
    {
        var url = $"pages/{_configuration.PageId}/incidents/{incident.Id}";
        using var client = _httpClientService.GetClient();

        var patchIncident = new PatchIncidentRequest {Incident = incident};
        
        var serialized = JsonSerializer.Serialize(patchIncident, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        
        var result = await client.PatchAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));

        return result.StatusCode;
    }

    public async Task<Incident?> CreateIncidentAsync(PostIncident incident)
    {
        var url = $"pages/{_configuration.PageId}/incidents";
        using var client = _httpClientService.GetClient();

        var request = new PostIncidentRequest
        {
            Incident = incident
        };
        
        var serialized = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        
        var result = await client.PostAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));
        var content = await result.Content.ReadAsStringAsync();

        if(result.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<Incident>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        throw new HttpRequestException(message: $"Failed to create incident: {content}",
            null,
            statusCode: result.StatusCode);
    }

    /// <summary>
    /// Configuration for the service.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The PageId of the status page to query
        /// </summary>
        public string PageId { get; init; } = null!;
        
        /// <summary>
        /// The API key to use for the status page
        /// </summary>
        public string ApiKey { get; init; } = null!;
        
        /// <summary>
        /// The base URL for the status page API
        /// </summary>
        public string ApiUrl { get; init; } = null!;
    }
}