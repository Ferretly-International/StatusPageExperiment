using System.Text.Json.Serialization;

namespace StatusPageLibrary.DTO;

/// <summary>
/// A simple object representing an incident
/// </summary>
public class Incident
{
    /// <summary>
    /// The ID of the incident
    /// </summary>
    public string Id { get; init; } = null!;

    /// <summary>
    /// The name of the incident
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// The current status of the incident
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPageLibrary.Models.Incident.StatusEnum Status { get; init; }
        
    /// <summary>
    /// The impact of the incident
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPageLibrary.Models.Incident.ImpactEnum Impact { get; init; }

    /// <summary>
    /// When the incident was created
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// When the incident was last updated
    /// </summary>
    public DateTime UpdatedAt { get; init; }
        
    /// <summary>
    /// A link to the incident's status page
    /// </summary>
    public string IncidentUrl { get; init; } = null!;
}