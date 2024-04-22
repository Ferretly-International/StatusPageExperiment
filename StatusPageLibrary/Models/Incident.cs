/*
 * Statuspage API
 *
 * # Code of Conduct Please don't abuse the API, and please report all feature requests and
 * issues to https://support.atlassian.com/contact
 *
 * # Rate Limiting Each
 *
 * API token is limited to 1 request / second as measured on a 60 second rolling window.
 * To get this limit increased, please contact us at https://support.atlassian.com/contact
 * Error codes 420 or 429 indicate that you have exceeded the rate limit and the request has been rejected.
 *
 * # Basics
 *
 * ## HTTPS
 *
 * It's required
 *
 * ## URL Prefix
 *
 * In order to maintain version integrity into the future, the API is versioned.
 * All calls currently begin with the following prefix:
 * https://api.statuspage.io/v1/
 *
 * ## RESTful Interface
 *
 * Wherever possible, the API seeks to implement repeatable patterns with logical,
 * representative URLs and descriptive HTTP verbs. Below are some examples and conventions
 * you will see throughout the documentation.
 *  * Collections are buckets: https://api.statuspage.io/v1/pages/asdf123/incidents.json
 *  * Elements have unique IDs: https://api.statuspage.io/v1/pages/asdf123/incidents/jklm456.json
 *  * GET will retrieve information about a collection/element
 *  * POST will create an element in a collection
 *  * PATCH will update a single element
 *  * PUT will replace a single element in a collection (rarely used)
 *  * DELETE will destroy a single element  ## Sending Data Information can be sent in the body as form urlencoded or JSON,
 *    but make sure the Content-Type header matches the body structure or the server gremlins will be angry.
 *
 * The version of the OpenAPI document: 1.0.0
 *
 * Generated by: https://openapi-generator.tech
 */

using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace StatusPageLibrary.Models
{ 
    /// <summary>
    /// Incident model. Not used for updates, use <see cref="PatchIncident"/> instead
    /// </summary>
    [DataContract]
    public class Incident
    {
        /// <summary>
        /// Incident Identifier
        /// </summary>
        /// <value>Incident Identifier</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Incident components
        /// </summary>
        /// <value>Incident components</value>
        [DataMember(Name="components", EmitDefaultValue=false)]
        public List<Component> Components { get; set; }

        /// <summary>
        /// The timestamp when the incident was created at.
        /// </summary>
        /// <value>The timestamp when the incident was created at.</value>
        [DataMember(Name="created_at", EmitDefaultValue=false)]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// The impact of the incident.
        /// </summary>
        /// <value>The impact of the incident.</value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ImpactEnum
        {
            
            /// <summary>
            /// Enum NoneEnum for none
            /// </summary>
            none = 1,
            
            /// <summary>
            /// Enum MaintenanceEnum for maintenance
            /// </summary>
            maintenance = 2,
            
            /// <summary>
            /// Enum MinorEnum for minor
            /// </summary>
            minor = 3,
            
            /// <summary>
            /// Enum MajorEnum for major
            /// </summary>
            major = 4,
            
            /// <summary>
            /// Enum CriticalEnum for critical
            /// </summary>
            critical = 5
        }

        /// <summary>
        /// The impact of the incident.
        /// </summary>
        /// <value>The impact of the incident.</value>
        [DataMember(Name="impact", EmitDefaultValue=false)]
        public ImpactEnum Impact { get; set; }


        /// <summary>
        /// value to override calculated impact value
        /// </summary>
        /// <value>value to override calculated impact value</value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ImpactOverrideEnum
        {
            empty = 0, 
        
            /// <summary>
            /// Enum NoneEnum for none
            /// </summary>
            none = 1,
            
            /// <summary>
            /// Enum MaintenanceEnum for maintenance
            /// </summary>
            maintenance = 2,
            
            /// <summary>
            /// Enum MinorEnum for minor
            /// </summary>
            minor = 3,
            
            /// <summary>
            /// Enum MajorEnum for major
            /// </summary>
            major = 4,
            
            /// <summary>
            /// Enum CriticalEnum for critical
            /// </summary>
            critical = 5,
            
            
        }

        /// <summary>
        /// The incident updates for incident.
        /// </summary>
        /// <value>The incident updates for incident.</value>
        [DataMember(Name="incident_updates", EmitDefaultValue=false)]
        [JsonPropertyName("incident_updates")]
        public List<IncidentUpdate> IncidentUpdates { get; set; }

        /// <summary>
        /// Metadata attached to the incident. Top level values must be objects.
        /// </summary>
        /// <value>Metadata attached to the incident. Top level values must be objects.</value>
        [DataMember(Name="metadata", EmitDefaultValue=false)]
        public Object Metadata { get; set; }

        /// <summary>
        /// The timestamp when incident entered monitoring state.
        /// </summary>
        /// <value>The timestamp when incident entered monitoring state.</value>
        [DataMember(Name="monitoring_at", EmitDefaultValue=false)]
        [JsonPropertyName("monitoring_at")]
        public DateTime? MonitoringAt { get; set; }

        /// <summary>
        /// Incident Name. There is a maximum limit of 255 characters.
        /// </summary>
        /// <value>Incident Name. There is a maximum limit of 255 characters.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Incident Page Identifier
        /// </summary>
        /// <value>Incident Page Identifier</value>
        [DataMember(Name="page_id", EmitDefaultValue=false)]
        [JsonPropertyName("page_id")]
        public string PageId { get; set; }

        /// <summary>
        /// Body of the Postmortem.
        /// </summary>
        /// <value>Body of the Postmortem.</value>
        [DataMember(Name="postmortem_body", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_body")]
        public string PostmortemBody { get; set; }

        /// <summary>
        /// The timestamp when the incident postmortem body was last updated at.
        /// </summary>
        /// <value>The timestamp when the incident postmortem body was last updated at.</value>
        [DataMember(Name="postmortem_body_last_updated_at", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_body_last_updated_at")]
        public DateTime? PostmortemBodyLastUpdatedAt { get; set; }

        /// <summary>
        /// Controls whether the incident will have postmortem.
        /// </summary>
        /// <value>Controls whether the incident will have postmortem.</value>
        [DataMember(Name="postmortem_ignored", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_ignored")]
        public bool PostmortemIgnored { get; set; }

        /// <summary>
        /// Indicates whether subscribers are already notified about postmortem.
        /// </summary>
        /// <value>Indicates whether subscribers are already notified about postmortem.</value>
        [DataMember(Name="postmortem_notified_subscribers", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_notified_subscribers")]
        public bool PostmortemNotifiedSubscribers { get; set; }

        /// <summary>
        /// Controls whether to decide if notify postmortem on twitter.
        /// </summary>
        /// <value>Controls whether to decide if notify postmortem on twitter.</value>
        [DataMember(Name="postmortem_notified_twitter", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_notified_twitter")]
        public bool PostmortemNotifiedTwitter { get; set; }

        /// <summary>
        /// The timestamp when the postmortem was published.
        /// </summary>
        /// <value>The timestamp when the postmortem was published.</value>
        [DataMember(Name="postmortem_published_at", EmitDefaultValue=false)]
        [JsonPropertyName("postmortem_published_at")]
        public DateTime? PostmortemPublishedAt { get; set; }

        /// <summary>
        /// The timestamp when incident was resolved.
        /// </summary>
        /// <value>The timestamp when incident was resolved.</value>
        [DataMember(Name="resolved_at", EmitDefaultValue=false)]
        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }

        /// <summary>
        /// Controls whether the incident is scheduled to automatically change to complete.
        /// </summary>
        /// <value>Controls whether the incident is scheduled to automatically change to complete.</value>
        [DataMember(Name="scheduled_auto_completed", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_auto_completed")]
        public bool ScheduledAutoCompleted { get; set; }

        /// <summary>
        /// Controls whether the incident is scheduled to automatically change to in progress.
        /// </summary>
        /// <value>Controls whether the incident is scheduled to automatically change to in progress.</value>
        [DataMember(Name="scheduled_auto_in_progress", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_auto_in_progress")]
        public bool ScheduledAutoInProgress { get; set; }

        /// <summary>
        /// The timestamp the incident is scheduled for.
        /// </summary>
        /// <value>The timestamp the incident is scheduled for.</value>
        [DataMember(Name="scheduled_for", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_for")]
        public DateTime? ScheduledFor { get; set; }

        /// <summary>
        /// Controls whether send notification when scheduled maintenances auto transition to completed.
        /// </summary>
        /// <value>Controls whether send notification when scheduled maintenances auto transition to completed.</value>
        [DataMember(Name="auto_transition_deliver_notifications_at_end", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_deliver_notifications_at_end")]
        public bool AutoTransitionDeliverNotificationsAtEnd { get; set; }

        /// <summary>
        /// Controls whether send notification when scheduled maintenances auto transition to started.
        /// </summary>
        /// <value>Controls whether send notification when scheduled maintenances auto transition to started.</value>
        [DataMember(Name="auto_transition_deliver_notifications_at_start", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_deliver_notifications_at_start")]
        public bool AutoTransitionDeliverNotificationsAtStart { get; set; }

        /// <summary>
        /// Controls whether change components status to under_maintenance once scheduled maintenance is in progress.
        /// </summary>
        /// <value>Controls whether change components status to under_maintenance once scheduled maintenance is in progress.</value>
        [DataMember(Name="auto_transition_to_maintenance_state", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_to_maintenance_state")]
        public bool AutoTransitionToMaintenanceState { get; set; }

        /// <summary>
        /// Controls whether change components status to operational once scheduled maintenance completes.
        /// </summary>
        /// <value>Controls whether change components status to operational once scheduled maintenance completes.</value>
        [DataMember(Name="auto_transition_to_operational_state", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_to_operational_state")]
        public bool AutoTransitionToOperationalState { get; set; }

        /// <summary>
        /// Controls whether to remind subscribers prior to scheduled incidents.
        /// </summary>
        /// <value>Controls whether to remind subscribers prior to scheduled incidents.</value>
        [DataMember(Name="scheduled_remind_prior", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_remind_prior")]
        public bool ScheduledRemindPrior { get; set; }

        /// <summary>
        /// The timestamp when the scheduled incident reminder was sent at.
        /// </summary>
        /// <value>The timestamp when the scheduled incident reminder was sent at.</value>
        [DataMember(Name="scheduled_reminded_at", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_reminded_at")]
        public DateTime? ScheduledRemindedAt { get; set; }

        /// <summary>
        /// The timestamp the incident is scheduled until.
        /// </summary>
        /// <value>The timestamp the incident is scheduled until.</value>
        [DataMember(Name="scheduled_until", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_until")]
        public DateTime? ScheduledUntil { get; set; }

        /// <summary>
        /// Incident Shortlink.
        /// </summary>
        /// <value>Incident Shortlink.</value>
        [DataMember(Name="shortlink", EmitDefaultValue=false)]
        public string Shortlink { get; set; }


        /// <summary>
        /// The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.
        /// </summary>
        /// <value>The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.</value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum InvestigatingEnum for investigating
            /// </summary>
            investigating = 1,
            
            /// <summary>
            /// Enum IdentifiedEnum for identified
            /// </summary>
            identified = 2,
            
            /// <summary>
            /// Enum MonitoringEnum for monitoring
            /// </summary>
            monitoring = 3,
            
            /// <summary>
            /// Enum ResolvedEnum for resolved
            /// </summary>
            resolved = 4,
            
            /// <summary>
            /// Enum ScheduledEnum for scheduled
            /// </summary>
            scheduled = 5,
            
            /// <summary>
            /// Enum InProgressEnum for in_progress
            /// </summary>
            in_progress = 6,
            
            /// <summary>
            /// Enum VerifyingEnum for verifying
            /// </summary>
            verifying = 7,
            
            /// <summary>
            /// Enum CompletedEnum for completed
            /// </summary>
            completed = 8
        }

        /// <summary>
        /// The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.
        /// </summary>
        /// <value>The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// The timestamp when the incident was updated at.
        /// </summary>
        /// <value>The timestamp when the incident was updated at.</value>
        [DataMember(Name="updated_at", EmitDefaultValue=false)]
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Custom reminder intervals for unresolved/open incidents. Not applicable for &lt;strong&gt;Scheduled maintenance&lt;/strong&gt;&lt;br&gt;There are 4 possible states for reminder_intervals:&lt;br&gt;&lt;strong&gt;DEFAULT:&lt;/strong&gt; NULL, representing a default behavior with intervals [3, 6, 12, 24].&lt;br&gt;&lt;strong&gt;AFTER:&lt;/strong&gt; A serialized array of strictly increasing intervals, each integer ranges from [1-24] (inclusive). Ex \&quot;[1, 5, 7, 10]\&quot;&lt;br&gt;&lt;strong&gt;EVERY:&lt;/strong&gt; An integer in the range [1-24] as a string, representing equal intervals. Ex \&quot;4\&quot; for [4, 8, 12, 16, 20, 24]&lt;br&gt;&lt;strong&gt;OFF:&lt;/strong&gt; A serialized empty array, for example, \&quot;[]\&quot;, meaning no reminder notifications will be sent.
        /// </summary>
        /// <value>Custom reminder intervals for unresolved/open incidents. Not applicable for &lt;strong&gt;Scheduled maintenance&lt;/strong&gt;&lt;br&gt;There are 4 possible states for reminder_intervals:&lt;br&gt;&lt;strong&gt;DEFAULT:&lt;/strong&gt; NULL, representing a default behavior with intervals [3, 6, 12, 24].&lt;br&gt;&lt;strong&gt;AFTER:&lt;/strong&gt; A serialized array of strictly increasing intervals, each integer ranges from [1-24] (inclusive). Ex \&quot;[1, 5, 7, 10]\&quot;&lt;br&gt;&lt;strong&gt;EVERY:&lt;/strong&gt; An integer in the range [1-24] as a string, representing equal intervals. Ex \&quot;4\&quot; for [4, 8, 12, 16, 20, 24]&lt;br&gt;&lt;strong&gt;OFF:&lt;/strong&gt; A serialized empty array, for example, \&quot;[]\&quot;, meaning no reminder notifications will be sent.</value>
        [DataMember(Name="reminder_intervals", EmitDefaultValue=false)]
        [JsonPropertyName("reminder_intervals")]
        public string ReminderIntervals { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Incident {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Components: ").Append(Components).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  Impact: ").Append(Impact).Append("\n");
            sb.Append("  IncidentUpdates: ").Append(IncidentUpdates).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("  MonitoringAt: ").Append(MonitoringAt).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  PageId: ").Append(PageId).Append("\n");
            sb.Append("  PostmortemBody: ").Append(PostmortemBody).Append("\n");
            sb.Append("  PostmortemBodyLastUpdatedAt: ").Append(PostmortemBodyLastUpdatedAt).Append("\n");
            sb.Append("  PostmortemIgnored: ").Append(PostmortemIgnored).Append("\n");
            sb.Append("  PostmortemNotifiedSubscribers: ").Append(PostmortemNotifiedSubscribers).Append("\n");
            sb.Append("  PostmortemNotifiedTwitter: ").Append(PostmortemNotifiedTwitter).Append("\n");
            sb.Append("  PostmortemPublishedAt: ").Append(PostmortemPublishedAt).Append("\n");
            sb.Append("  ResolvedAt: ").Append(ResolvedAt).Append("\n");
            sb.Append("  ScheduledAutoCompleted: ").Append(ScheduledAutoCompleted).Append("\n");
            sb.Append("  ScheduledAutoInProgress: ").Append(ScheduledAutoInProgress).Append("\n");
            sb.Append("  ScheduledFor: ").Append(ScheduledFor).Append("\n");
            sb.Append("  AutoTransitionDeliverNotificationsAtEnd: ").Append(AutoTransitionDeliverNotificationsAtEnd).Append("\n");
            sb.Append("  AutoTransitionDeliverNotificationsAtStart: ").Append(AutoTransitionDeliverNotificationsAtStart).Append("\n");
            sb.Append("  AutoTransitionToMaintenanceState: ").Append(AutoTransitionToMaintenanceState).Append("\n");
            sb.Append("  AutoTransitionToOperationalState: ").Append(AutoTransitionToOperationalState).Append("\n");
            sb.Append("  ScheduledRemindPrior: ").Append(ScheduledRemindPrior).Append("\n");
            sb.Append("  ScheduledRemindedAt: ").Append(ScheduledRemindedAt).Append("\n");
            sb.Append("  ScheduledUntil: ").Append(ScheduledUntil).Append("\n");
            sb.Append("  Shortlink: ").Append(Shortlink).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  ReminderIntervals: ").Append(ReminderIntervals).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, 
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                });
        }
        
        /// <summary>
        /// Convert the Incident object to a DTO
        /// </summary>
        /// <returns></returns>
        public DTO.Incident AsDto()
        {
            return new DTO.Incident
            {
                Id = Id,
                Name = Name,
                Status = Status,
                Impact = Impact,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                IncidentUrl = Shortlink
            };
        }
    }
}
