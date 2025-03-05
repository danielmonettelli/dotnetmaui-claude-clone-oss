namespace Claude.Models;

/// <summary>
/// Represents the response received from the Anthropic API after processing a chat completion request.
/// </summary>
/// <example>
/// {
///   "content": [{ "text": "Hi! My name is Claude.", "type": "text" }],
///   "id": "msg_013Zva2CMHLNnXjNJJKqJ2EF",
///   "model": "claude-3-5-sonnet-20241022",
///   "role": "assistant",
///   "stop_reason": "end_turn",
///   "stop_sequence": null,
///   "type": "message",
///   "usage": { "input_tokens": 2095, "output_tokens": 503 }
/// }
/// </example>
public class AnthropicResponse
{
    /// <summary>
    /// The unique identifier for this response message
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of the response, typically "message"
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The role of the entity that generated this response, typically "assistant"
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The list of content blocks in the response, containing the actual message content
    /// </summary>
    [JsonPropertyName("content")]
    public List<MessageContent> Content { get; set; } = [];

    /// <summary>
    /// The model used to generate this response (e.g., "claude-3-5-sonnet-20241022")
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The reason why the response generation stopped (e.g., "end_turn")
    /// </summary>
    [JsonPropertyName("stop_reason")]
    public string StopReason { get; set; } = string.Empty;

    /// <summary>
    /// The sequence that caused the response to stop, if any
    /// </summary>
    [JsonPropertyName("stop_sequence")]
    public string? StopSequence { get; set; }

    /// <summary>
    /// Token usage statistics for this response
    /// </summary>
    [JsonPropertyName("usage")]
    public Usage Usage { get; set; } = new();
}

/// <summary>
/// Represents a content block within an Anthropic API response.
/// Contains the actual message content and its type.
/// </summary>
public class MessageContent
{
    /// <summary>
    /// The type of content in this block, typically "text"
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The text content of this block
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Contains token usage statistics for an API request and response.
/// Used for tracking API consumption and costs.
/// </summary>
public class Usage
{
    /// <summary>
    /// The number of tokens used in the input/prompt
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    /// <summary>
    /// The number of tokens generated in the output/response
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }
}
