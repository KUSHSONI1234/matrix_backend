using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ShiftBackend.ValidationAttributes;

namespace ShiftBackend.Models
{
    public class ShiftModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Time is required.")]
        [JsonPropertyName("startTime")]
        public string StartTime { get; set; } = string.Empty;

        [Required(ErrorMessage = "End Time is required.")]
        [JsonPropertyName("endTime")]
        public string EndTime { get; set; } = string.Empty;

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("attendanceType")]
        public string? AttendanceType { get; set; }

        [JsonPropertyName("halfDay")]
        public string? HalfDay { get; set; }

        [JsonPropertyName("fullDay")]
        public string? FullDay { get; set; }

        [JsonPropertyName("minHoursRequired")]
        public bool? MinHoursRequired { get; set; }

        [JsonPropertyName("deviationStart")]
        public int? DeviationStart { get; set; }

        [JsonPropertyName("deviationEnd")]
        public int? DeviationEnd { get; set; }

        [JsonPropertyName("shiftAllowance")]
        public bool? ShiftAllowance { get; set; }

        // Breaks
        [JsonPropertyName("breakStart")]
        public string? BreakStart { get; set; }

        [JsonPropertyName("breakEnd")]
        public string? BreakEnd { get; set; }

        [JsonPropertyName("breakDuration")]
        public string? BreakDuration { get; set; }

        // Deviation
        [JsonPropertyName("deviationAllowed")]
        public bool DeviationAllowed { get; set; }

        [JsonPropertyName("addLateIn")]
        public bool AddLateIn { get; set; }

        [JsonPropertyName("addEarlyOut")]
        public bool AddEarlyOut { get; set; }

        // Deduction
        [JsonPropertyName("deduct2Punch")]
        public bool Deduct2Punch { get; set; }

        [RequiredIfTrue("Deduct2Punch", ErrorMessage = "Deduct2PunchType is required when Deduct2Punch is true.")]
        [JsonPropertyName("deduct2PunchType")]
        public string? Deduct2PunchType { get; set; }

        [JsonPropertyName("deduct2PlusPunch")]
        public bool Deduct2PlusPunch { get; set; }

        [RequiredIfTrue("Deduct2PlusPunch", ErrorMessage = "Deduct2PlusPunchType is required when Deduct2PlusPunch is true.")]
        [JsonPropertyName("deduct2PlusPunchType")]
        public string? Deduct2PlusPunchType { get; set; }

        // Grace Time
        [JsonPropertyName("includeGraceTime")]
        public bool IncludeGraceTime { get; set; }

        [JsonPropertyName("shiftLateIn")]
        public int? ShiftLateIn { get; set; }

        [JsonPropertyName("shiftLateInOverlap")]
        public bool ShiftLateInOverlap { get; set; }

        [JsonPropertyName("shiftEarlyOut")]
        public int? ShiftEarlyOut { get; set; }

        [JsonPropertyName("shiftEarlyOutOverlap")]
        public bool ShiftEarlyOutOverlap { get; set; }

        [JsonPropertyName("breakLateIn")]
        public int? BreakLateIn { get; set; }

        [JsonPropertyName("breakLateInOverlap")]
        public bool BreakLateInOverlap { get; set; }

        [JsonPropertyName("breakEarlyOut")]
        public int? BreakEarlyOut { get; set; }

        [JsonPropertyName("breakEarlyOutOverlap")]
        public bool BreakEarlyOutOverlap { get; set; }
    }
}
