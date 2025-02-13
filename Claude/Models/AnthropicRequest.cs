using System.Text.Json.Serialization;

namespace Claude.Models;

/// <summary>
/// Represents a request to the Anthropic API
/// </summary>
public class AnthropicRequest
{
    /// <summary>
    /// The Claude model to use for the request
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = "claude-3-5-sonnet-20241022";

    /// <summary>
    /// The maximum number of tokens to generate in the response
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1024;

    /// <summary>
    /// The list of messages in the conversation
    /// </summary>
    [JsonPropertyName("messages")]
    public List<ChatMessage> Messages { get; set; } = new();
}
