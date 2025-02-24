using System.Text.Json.Serialization;

namespace BookOrGetBooked.Shared.Utilities
{
    public class ErrorDetails
    {
        public string Code { get; }
        public string Message { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; } // E.g., the field or resource related to the error

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? AdditionalData { get; } // To provide extra context

        public ErrorDetails(string code, string message, string? target = null, Dictionary<string, object>? additionalData = null)
        {
            Code = code;
            Message = message;
            Target = target;
            AdditionalData = additionalData;
        }
    }
}
