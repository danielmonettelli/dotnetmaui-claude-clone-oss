namespace Claude.Models;

/// <summary>
/// Represents a request to the Anthropic API for chat completion.
/// This class encapsulates all the parameters needed to make a successful API call.
/// </summary>
/// <example>
/// {
///     "model": "claude-3-5-sonnet-20241022",
///     "max_tokens": 1024,
///     "messages": [
///         {"role": "user", "content": "Hello, world"}
///     ]
/// }
/// </example>
public class AnthropicRequest
{
    /// <summary>
    /// The Claude model identifier to use for generating the response.
    /// Default is "claude-3-5-sonnet-20241022".
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = "claude-3-5-sonnet-20241022";

    /// <summary>
    /// The maximum number of tokens that can be generated in the response.
    /// This helps control the length of the generated content.
    /// Default is 1024 tokens.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1024;

    /// <summary>
    /// The conversation messages to be processed by the model.
    /// Each message contains a role (e.g., "user") and content.
    /// </summary>
    [JsonPropertyName("messages")]
    public List<ChatMessage> Messages { get; set; } = [];
}
