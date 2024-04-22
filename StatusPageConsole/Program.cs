using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StatusPageLibrary;
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

var serviceConfiguration = config.GetSection("StatusPage").Get<IncidentsService.Configuration>();
Console.WriteLine($"StatusPage:ApiUrl: {serviceConfiguration.ApiUrl}");
Console.WriteLine($"StatusPage:ApiKey: {serviceConfiguration.ApiKey}");

await using var serviceProvider = new ServiceCollection()
    .AddLogging(cfg => { cfg
        .ClearProviders()
        .AddConsole().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
    })
    .AddSingleton<IConfiguration>(config)
    .AddStatusPageLibrary(serviceConfiguration)
    .BuildServiceProvider();

// get the built IIncidentsService
var incidentsService = serviceProvider.GetService<IIncidentsService>();
if (incidentsService == null)
{
    Console.WriteLine("Failed to get IIncidentsService");
    return;
}

var activeIncidents = await incidentsService.GetActiveIncidentsAsync();
Console.WriteLine("There are {0} active incidents", activeIncidents?.Count ?? 0);

// get incident history
var incidentHistory = await incidentsService.GetIncidentHistoryAsync(); 
Console.WriteLine("There are {0} incidents in history", incidentHistory?.Count ?? 0);
incidentHistory?.ForEach(incident => Console.WriteLine($"{incident.Name} ({incident.Id})"));

// create a new incident
var newIncident = new PostIncident
{
    Status = PostIncident.StatusEnum.identified,
    ImpactOverride = PostIncident.ImpactOverrideEnum.major.ToString(),
    Name = $"New Incident {DateTime.Now:g}",
    Body = "This is a new incident that we created from the StatusPageConsole",
    Components =
    {
        // TODO - the component id should be fetched from configuration as they change 
        // depending on the status page instance being used
        ["kjtk74jtlcrr"] = Component.StatusEnum.major_outage.ToString()
    }
};

newIncident.ComponentIds.Add("kjtk74jtlcrr");

var createdIncident = await incidentsService.CreateIncidentAsync(newIncident);
Console.WriteLine("Created incident {0}", createdIncident?.Id);

if (createdIncident != null)
{
    var incident = await incidentsService.GetIncidentAsync(createdIncident.Id);
    Console.WriteLine("Got newly created incident {0}", incident?.Id);
}

// get active incidents
activeIncidents = await incidentsService.GetActiveIncidentsAsync();

Console.WriteLine("There are now {0} active incidents", activeIncidents?.Count ?? 0);
activeIncidents?.ForEach(incident => Console.WriteLine($"{incident.Name} ({incident.Id}): {incident.Status} @ {incident.Shortlink}"));

// resolve the incident created above
if (createdIncident != null)
{
    Console.WriteLine("Updating incident {0}", createdIncident.Id);

    var patchIncident = new PatchIncident
    {
        Id = createdIncident.Id,
        Status = Incident.StatusEnum.investigating,
        Body = "We are investigating the issue",
        DeliverNotifications = false
    };

    createdIncident.Components.ForEach(component =>
    {
        patchIncident.Components[component.Id] = component.Status.ToString();    
    });
    
    var resultIncident = await incidentsService.UpdateIncidentAsync(patchIncident);
    Console.WriteLine($"Update result: {resultIncident.IncidentUpdates.Count} updates");
}

