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
    
    public string Id { get; set; } = string.Empty;
    
    public Incident.StatusEnum Status { get; set; } 

    /// <summary>
    /// An optional update message, created as the first incident update. There is a maximum limit of 25000 characters.
    /// </summary>
    /// <remarks>If not provided, a default message based upon the <see cref="Status"/> will be displayed</remarks>
    public string Body { get; set; } = string.Empty;
    
    /// <summary>
    /// Dictionary of components and their new status. The key is the component id and the value is the new status.
    /// </summary>
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
