// See https://aka.ms/new-console-template for more information

// C:\Users\hokie\gen\src\Org.OpenAPITools

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StatusPageLibrary.Models;
using StatusPageLibrary.Services;

Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
    .AddAzureAppConfiguration(options => { options
        .Connect(Environment.GetEnvironmentVariable("AppConfigConnectionString"))
        // Load configuration values with no label
        .Select(KeyFilter.Any)
        // Override with any configuration values specific to current hosting env
        .Select(KeyFilter.Any, "Development");
    })
    .Build();

Console.WriteLine($"StatusPage:ApiUrl: {config["StatusPage:ApiUrl"]}");
Console.WriteLine($"StatusPage:ApiKey: {config["StatusPage:ApiKey"]}");

await using var serviceProvider = new ServiceCollection()
    .AddLogging(cfg => { cfg
        .ClearProviders()
        .AddConsole().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
    })
    .AddSingleton<IConfiguration>(config)
    .AddHttpClient()
    // add services
    .AddSingleton<IHttpClientService, HttpClientService>()
    .AddSingleton<IIncidentsService, IncidentsService>()
    .BuildServiceProvider();

// get the built IIncidentsService
var incidentsService = serviceProvider.GetService<IIncidentsService>();
if (incidentsService == null)
{
    Console.WriteLine("Failed to get IIncidentsService");
    return;
}

// create a new incident
var newIncident = new PostIncident
{
    Status = PostIncident.StatusEnum.investigating,
    Name = $"New Incident {DateTime.Now:g}",
    Body = "This is a new incident that we created from the StatusPageConsole",
};

var createdIncident = await incidentsService.CreateIncidentAsync(newIncident);
Console.WriteLine("Created incident {0}", createdIncident?.Id);

// get active incidents
var activeIncidents = await incidentsService.GetActiveIncidentsAsync();

Console.WriteLine("There are {0} active incidents", activeIncidents?.Count ?? 0);

// get incident history
var incidentHistory = await incidentsService.GetIncidentHistoryAsync(); 
Console.WriteLine("There are {0} incidents in history", incidentHistory?.Count ?? 0);
incidentHistory?.ForEach(incident => Console.WriteLine(incident.Name));

// update the first active incident
var firstActiveIncident = activeIncidents?.FirstOrDefault();
if (firstActiveIncident != null)
{
    Console.WriteLine("Updating incident {0}", firstActiveIncident.Id);

    var patchIncident = new PatchIncident
    {
        Id = firstActiveIncident.Id,
        Status = Incident.StatusEnum.resolved,
        Body = "Finally back online!",
        ImpactOverride = Incident.ImpactOverrideEnum.none,
        DeliverNotifications = true
    };

    firstActiveIncident.Components.ForEach(component =>
    {
        patchIncident.Components[component.Id] = Component.StatusEnum.operational.ToString();    
    });
    
    var result = await incidentsService.UpdateIncidentAsync(patchIncident);
    Console.WriteLine("Update result: {0}", result);
}

