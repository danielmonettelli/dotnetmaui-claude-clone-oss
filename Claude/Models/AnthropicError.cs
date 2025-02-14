using System.Text.Json.Serialization;

namespace Claude.Models;

/// <summary>
/// Represents an error response from the Anthropic API.
/// </summary>
/// <example>
/// {
///   "type": "error",
///   "error": {
///     "type": "invalid_request_error",
///     "message": "Invalid request"
///   }
/// }
/// </example>
public class AnthropicErrorResponse
{
    /// <summary>
    /// The type of response, will be "error" for error responses
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The detailed error information
    /// </summary>
    [JsonPropertyName("error")]
    public AnthropicErrorDetail Error { get; set; } = new();
}

/// <summary>
/// Contains the detailed error information from the Anthropic API
/// </summary>
public class AnthropicErrorDetail
{
    /// <summary>
    /// The specific type of error that occurred
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The human-readable error message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
