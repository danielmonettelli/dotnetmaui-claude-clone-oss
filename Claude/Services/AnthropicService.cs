using Claude.Constants;
using Claude.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Claude.Services;

/// <summary>
/// Provides communication services with the Anthropic API for chat completions.
/// Implements the IAnthropicService interface for handling message requests and responses.
/// </summary>
public class AnthropicService : IAnthropicService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the AnthropicService with the specified HTTP client.
    /// Configures the base URL, API key, and JSON serialization options.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for API communication</param>
    public AnthropicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(ApiConstants.ANTHROPIC_BASE_URL);

        // Required headers for Anthropic API as per documentation
        _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiConstants.ANTHROPIC_API_KEY);
        _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        // Note: These JSON options are currently redundant since model properties use [JsonPropertyName] attributes.
        // However, they are kept for:
        // 1. Future properties that might not have explicit attributes
        // 2. Maintaining consistency across the project
        // 3. Supporting additional model classes without attributes
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// Sends a chat message to the Anthropic API and returns the response content.
    /// </summary>
    /// <param name="query">The message content to send</param>
    /// <returns>The response content from the API</returns>
    /// <exception cref="AnthropicException">Thrown when the API returns an error response</exception>
    /// <exception cref="InvalidOperationException">Thrown when unable to deserialize the response</exception>
    public async Task<string> SendChatMessageAsync(string query)
    {
        var request = new AnthropicRequest
        {
            Messages = new List<ChatMessage>
            {
                new() { Role = "user", Content = query }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(ApiConstants.MESSAGES_ENDPOINT, request, _jsonOptions);

        // If not successful, try to parse error response
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<AnthropicErrorResponse>(_jsonOptions);
            if (errorResponse != null)
            {
                throw new AnthropicException(
                    errorResponse.Error.Message,
                    errorResponse.Error.Type,
                    (int)response.StatusCode
                );
            }

            // If we can't parse the error, throw with the status code
            response.EnsureSuccessStatusCode();
        }

        var result = await response.Content.ReadFromJsonAsync<AnthropicResponse>(_jsonOptions)
                    ?? throw new InvalidOperationException("Failed to deserialize the response");

        return result.Content.FirstOrDefault()?.Text ?? string.Empty;
    }
}

/// <summary>
/// Represents an exception that occurs when interacting with the Anthropic API
/// </summary>
public class AnthropicException : Exception
{
    /// <summary>
    /// The type of error returned by the API
    /// </summary>
    public string ErrorType { get; }

    /// <summary>
    /// The HTTP status code returned by the API
    /// </summary>
    public int StatusCode { get; }

    public AnthropicException(string message, string errorType, int statusCode)
        : base(message)
    {
        ErrorType = errorType;
        StatusCode = statusCode;
    }
}
