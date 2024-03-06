﻿using System.Text.Json;
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
        var client = _httpClientService.GetClient();
        var result = await client
            .GetAsync($"pages/{_configuration["StatusPage:PageId"]}/incidents/unresolved");
        
        if (!result.IsSuccessStatusCode) throw new HttpRequestException(message: "Failed to retrieve active incidents",
            null,
            statusCode: result.StatusCode);
        
        var content = await result.Content.ReadAsStringAsync();
        var incidents = JsonSerializer.Deserialize<List<Incident>>(content);
        return incidents ?? new List<Incident>();
    }
}