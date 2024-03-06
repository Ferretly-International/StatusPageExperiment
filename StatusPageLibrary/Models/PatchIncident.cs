using System.Text.Json.Serialization;

namespace StatusPageLibrary.Models;

/// <summary>
/// Request object when updating an incident
/// </summary>
public class PatchIncidentRequest
{
    public PatchIncident Incident { get; set; } = new();
}

/// <summary>
/// Class for updating an incident
/// </summary>
public class PatchIncident
{
    private Incident.ImpactOverrideEnum? _impactOverride;
    
    public string Id { get; set; }
    
    public Incident.StatusEnum Status { get; set; } 

    /// <summary>
    /// The initial message, created as the first incident update. There is a maximum limit of 25000 characters
    /// </summary>
    public string Body { get; set; } = string.Empty;
    
    public Dictionary<string, string> Components { get; } = new Dictionary<string, string>();
    
    [JsonPropertyName("deliver_notifications")]
    public bool DeliverNotifications { get; set; }

    [JsonPropertyName("impact_override")]
    public Incident.ImpactOverrideEnum? ImpactOverride
    {
        get => _impactOverride;
        set => _impactOverride = value == Incident.ImpactOverrideEnum.empty ? null : value;
    }
}
