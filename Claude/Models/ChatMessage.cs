using System.Text.Json.Serialization;

namespace Claude.Models;

/// <summary>
/// Represents a chat message in the Anthropic API conversation
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// The role of the message sender (e.g., "user", "assistant")
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The content of the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
