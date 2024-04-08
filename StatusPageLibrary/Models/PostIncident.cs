using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace StatusPageLibrary.Models
{
    public class PostIncidentRequest
    {
        public PostIncident Incident { get; set; } = new();
    }
    
    /// <summary>
    /// Class used to create a new incident
    /// </summary>
    [DataContract]
    public class PostIncident
    {
        /// <summary>
        /// Incident Name. There is a maximum limit of 255 characters.
        /// </summary>
        /// <value>Incident Name. There is a maximum limit of 255 characters.</value>
        [Required]
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }
        
        /// <summary>
        /// The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.
        /// </summary>
        /// <value>The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.</value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StatusEnum
        {
            // ReSharper disable InconsistentNaming
            
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
            
            // ReSharper restore InconsistentNaming
        }

        /// <summary>
        /// The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.
        /// </summary>
        /// <value>The incident status. For realtime incidents, valid values are investigating, identified, monitoring, and resolved. For scheduled incidents, valid values are scheduled, in_progress, verifying, and completed.</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum Status { get; set; }


        /// <summary>
        /// value to override calculated impact value
        /// </summary>
        /// <value>value to override calculated impact value</value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ImpactOverrideEnum
        {
            
            /// <summary>
            /// Enum NoneEnum for none
            /// </summary>
            [EnumMember(Value = "none")]
            none = 1,
            
            /// <summary>
            /// Enum MaintenanceEnum for maintenance
            /// </summary>
            [EnumMember(Value = "maintenance")]
            maintenance = 2,
            
            /// <summary>
            /// Enum MinorEnum for minor
            /// </summary>
            [EnumMember(Value = "minor")]
            minor = 3,
            
            /// <summary>
            /// Enum MajorEnum for major
            /// </summary>
            [EnumMember(Value = "major")]
            major = 4,
            
            /// <summary>
            /// Enum CriticalEnum for critical
            /// </summary>
            [EnumMember(Value = "critical")]
            critical = 5
        }

        /// <summary>
        /// value to override calculated impact value
        /// </summary>
        /// <value>value to override calculated impact value</value>
        [DataMember(Name="impact_override", EmitDefaultValue=false)]
        [JsonPropertyName("impact_override")]
        public string ImpactOverride { get; set; }

        /// <summary>
        /// The timestamp the incident is scheduled for.
        /// </summary>
        /// <value>The timestamp the incident is scheduled for.</value>
        [DataMember(Name="scheduled_for", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_for")]
        public DateTime? ScheduledFor { get; set; }

        /// <summary>
        /// The timestamp the incident is scheduled until.
        /// </summary>
        /// <value>The timestamp the incident is scheduled until.</value>
        [DataMember(Name="scheduled_until", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_until")]
        public DateTime? ScheduledUntil { get; set; }

        /// <summary>
        /// Controls whether to remind subscribers prior to scheduled incidents.
        /// </summary>
        /// <value>Controls whether to remind subscribers prior to scheduled incidents.</value>
        [DataMember(Name="scheduled_remind_prior", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_remind_prior")]
        public bool ScheduledRemindPrior { get; set; }

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
        /// Controls whether the incident is scheduled to automatically change to in progress.
        /// </summary>
        /// <value>Controls whether the incident is scheduled to automatically change to in progress.</value>
        [DataMember(Name="scheduled_auto_in_progress", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_auto_in_progress")]
        public bool ScheduledAutoInProgress { get; set; }

        /// <summary>
        /// Controls whether the incident is scheduled to automatically change to complete.
        /// </summary>
        /// <value>Controls whether the incident is scheduled to automatically change to complete.</value>
        [DataMember(Name="scheduled_auto_completed", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_auto_completed")]
        public bool ScheduledAutoCompleted { get; set; }

        /// <summary>
        /// Controls whether send notification when scheduled maintenances auto transition to started.
        /// </summary>
        /// <value>Controls whether send notification when scheduled maintenances auto transition to started.</value>
        [DataMember(Name="auto_transition_deliver_notifications_at_start", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_deliver_notifications_at_start")]
        public bool AutoTransitionDeliverNotificationsAtStart { get; set; }

        /// <summary>
        /// Controls whether send notification when scheduled maintenances auto transition to completed.
        /// </summary>
        /// <value>Controls whether send notification when scheduled maintenances auto transition to completed.</value>
        [DataMember(Name="auto_transition_deliver_notifications_at_end", EmitDefaultValue=false)]
        [JsonPropertyName("auto_transition_deliver_notifications_at_end")]
        public bool AutoTransitionDeliverNotificationsAtEnd { get; set; }

        /// <summary>
        /// Custom reminder intervals for unresolved/open incidents. Not applicable for &lt;strong&gt;Scheduled maintenance&lt;/strong&gt;&lt;br&gt;There are 4 possible states for reminder_intervals:&lt;br&gt;&lt;strong&gt;DEFAULT:&lt;/strong&gt; NULL, representing a default behavior with intervals [3, 6, 12, 24].&lt;br&gt;&lt;strong&gt;AFTER:&lt;/strong&gt; A serialized array of strictly increasing intervals, each integer ranges from [1-24] (inclusive). Ex \&quot;[1, 5, 7, 10]\&quot;&lt;br&gt;&lt;strong&gt;EVERY:&lt;/strong&gt; An integer in the range [1-24] as a string, representing equal intervals. Ex \&quot;4\&quot; for [4, 8, 12, 16, 20, 24]&lt;br&gt;&lt;strong&gt;OFF:&lt;/strong&gt; A serialized empty array, for example, \&quot;[]\&quot;, meaning no reminder notifications will be sent.
        /// </summary>
        /// <value>Custom reminder intervals for unresolved/open incidents. Not applicable for &lt;strong&gt;Scheduled maintenance&lt;/strong&gt;&lt;br&gt;There are 4 possible states for reminder_intervals:&lt;br&gt;&lt;strong&gt;DEFAULT:&lt;/strong&gt; NULL, representing a default behavior with intervals [3, 6, 12, 24].&lt;br&gt;&lt;strong&gt;AFTER:&lt;/strong&gt; A serialized array of strictly increasing intervals, each integer ranges from [1-24] (inclusive). Ex \&quot;[1, 5, 7, 10]\&quot;&lt;br&gt;&lt;strong&gt;EVERY:&lt;/strong&gt; An integer in the range [1-24] as a string, representing equal intervals. Ex \&quot;4\&quot; for [4, 8, 12, 16, 20, 24]&lt;br&gt;&lt;strong&gt;OFF:&lt;/strong&gt; A serialized empty array, for example, \&quot;[]\&quot;, meaning no reminder notifications will be sent.</value>
        [DataMember(Name="reminder_intervals", EmitDefaultValue=false)]
        [JsonPropertyName("reminder_intervals")]
        public string ReminderIntervals { get; set; }

        /// <summary>
        /// Attach a json object to the incident. All top-level values in the object must also be objects.
        /// </summary>
        /// <value>Attach a json object to the incident. All top-level values in the object must also be objects.</value>
        [DataMember(Name="metadata", EmitDefaultValue=false)]
        public Object Metadata { get; set; }

        /// <summary>
        /// Deliver notifications to subscribers if this is true. If this is false, create an incident without notifying customers.
        /// </summary>
        /// <value>Deliver notifications to subscribers if this is true. If this is false, create an incident without notifying customers.</value>
        [DataMember(Name="deliver_notifications", EmitDefaultValue=false)]
        [JsonPropertyName("deliver_notifications")]
        public bool DeliverNotifications { get; set; } = true;

        /// <summary>
        /// Controls whether tweet automatically when scheduled maintenance starts.
        /// </summary>
        /// <value>Controls whether tweet automatically when scheduled maintenance starts.</value>
        [DataMember(Name="auto_tweet_at_beginning", EmitDefaultValue=false)]
        [JsonPropertyName("auto_tweet_at_beginning")]
        public bool AutoTweetAtBeginning { get; set; }

        /// <summary>
        /// Controls whether tweet automatically when scheduled maintenance completes.
        /// </summary>
        /// <value>Controls whether tweet automatically when scheduled maintenance completes.</value>
        [DataMember(Name="auto_tweet_on_completion", EmitDefaultValue=false)]
        [JsonPropertyName("auto_tweet_on_completion")]
        public bool AutoTweetOnCompletion { get; set; }

        /// <summary>
        /// Controls whether tweet automatically when scheduled maintenance is created.
        /// </summary>
        /// <value>Controls whether tweet automatically when scheduled maintenance is created.</value>
        [DataMember(Name="auto_tweet_on_creation", EmitDefaultValue=false)]
        [JsonPropertyName("auto_tweet_on_creation")]
        public bool AutoTweetOnCreation { get; set; }

        /// <summary>
        /// Controls whether tweet automatically one hour before scheduled maintenance starts.
        /// </summary>
        /// <value>Controls whether tweet automatically one hour before scheduled maintenance starts.</value>
        [DataMember(Name="auto_tweet_one_hour_before", EmitDefaultValue=false)]
        [JsonPropertyName("auto_tweet_one_hour_before")]
        public bool AutoTweetOneHourBefore { get; set; }

        /// <summary>
        /// TimeStamp when incident was backfilled.
        /// </summary>
        /// <value>TimeStamp when incident was backfilled.</value>
        [DataMember(Name="backfill_date", EmitDefaultValue=false)]
        [JsonPropertyName("backfill_date")]
        public string BackfillDate { get; set; }

        /// <summary>
        /// Controls whether incident is backfilled. If true, components cannot be specified.
        /// </summary>
        /// <value>Controls whether incident is backfilled. If true, components cannot be specified.</value>
        [DataMember(Name="backfilled", EmitDefaultValue=false)]
        public bool Backfilled { get; set; }

        /// <summary>
        /// The initial message, created as the first incident update. There is a maximum limit of 25000 characters
        /// </summary>
        /// <value>The initial message, created as the first incident update. There is a maximum limit of 25000 characters</value>
        [DataMember(Name="body", EmitDefaultValue=false)]
        public string Body { get; set; }

        /// <summary>
        /// Dictionary of components and their new status. The key is the component id and the value is the new status.
        /// </summary>
        public Dictionary<string, string> Components { get; } = new Dictionary<string, string>();
        
        /// <summary>
        /// List of component_ids affected by this incident
        /// </summary>
        /// <value>List of component_ids affected by this incident</value>
        [DataMember(Name="component_ids", EmitDefaultValue=false)]
        [JsonPropertyName("component_ids")]
        public List<string> ComponentIds { get; set; } = new();

        /// <summary>
        /// Same as :scheduled_auto_transition_in_progress. Controls whether the incident is scheduled to automatically change to in progress.
        /// </summary>
        /// <value>Same as :scheduled_auto_transition_in_progress. Controls whether the incident is scheduled to automatically change to in progress.</value>
        [DataMember(Name="scheduled_auto_transition", EmitDefaultValue=false)]
        [JsonPropertyName("scheduled_auto_transition")]
        public bool ScheduledAutoTransition { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostPagesPageIdIncidentsIncident {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  ImpactOverride: ").Append(ImpactOverride).Append("\n");
            sb.Append("  ScheduledFor: ").Append(ScheduledFor).Append("\n");
            sb.Append("  ScheduledUntil: ").Append(ScheduledUntil).Append("\n");
            sb.Append("  ScheduledRemindPrior: ").Append(ScheduledRemindPrior).Append("\n");
            sb.Append("  AutoTransitionToMaintenanceState: ").Append(AutoTransitionToMaintenanceState).Append("\n");
            sb.Append("  AutoTransitionToOperationalState: ").Append(AutoTransitionToOperationalState).Append("\n");
            sb.Append("  ScheduledAutoInProgress: ").Append(ScheduledAutoInProgress).Append("\n");
            sb.Append("  ScheduledAutoCompleted: ").Append(ScheduledAutoCompleted).Append("\n");
            sb.Append("  AutoTransitionDeliverNotificationsAtStart: ").Append(AutoTransitionDeliverNotificationsAtStart).Append("\n");
            sb.Append("  AutoTransitionDeliverNotificationsAtEnd: ").Append(AutoTransitionDeliverNotificationsAtEnd).Append("\n");
            sb.Append("  ReminderIntervals: ").Append(ReminderIntervals).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("  DeliverNotifications: ").Append(DeliverNotifications).Append("\n");
            sb.Append("  AutoTweetAtBeginning: ").Append(AutoTweetAtBeginning).Append("\n");
            sb.Append("  AutoTweetOnCompletion: ").Append(AutoTweetOnCompletion).Append("\n");
            sb.Append("  AutoTweetOnCreation: ").Append(AutoTweetOnCreation).Append("\n");
            sb.Append("  AutoTweetOneHourBefore: ").Append(AutoTweetOneHourBefore).Append("\n");
            sb.Append("  BackfillDate: ").Append(BackfillDate).Append("\n");
            sb.Append("  Backfilled: ").Append(Backfilled).Append("\n");
            sb.Append("  Body: ").Append(Body).Append("\n");
            sb.Append("  Components: ").Append(Components).Append("\n");
            sb.Append("  ComponentIds: ").Append(ComponentIds).Append("\n");
            sb.Append("  ScheduledAutoTransition: ").Append(ScheduledAutoTransition).Append("\n");
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
    }
}
