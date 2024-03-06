using Microsoft.Extensions.Configuration;
using StatusPageLibrary.Models;

namespace StatusPageLibrary.Services;

public interface IIncidentsService
{
    List<Incident> GetActiveIncidents();
}

public class IncidentsService: IIncidentsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public IncidentsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public List<Incident> GetActiveIncidents()
    {
        return new List<Incident>();
    }
}