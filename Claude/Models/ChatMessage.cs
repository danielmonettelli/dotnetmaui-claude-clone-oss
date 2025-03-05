namespace Claude.Models;

/// <summary>
/// Represents a single message in the conversation with the Anthropic API.
/// Each message includes a role identifier and the message content.
/// </summary>
/// <example>
/// {"role": "user", "content": "Hello, world"}
/// </example>
public class ChatMessage
{
    /// <summary>
    /// The role of the message sender.
    /// Common values are "user" for user messages and "assistant" for AI responses.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The actual content/text of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
