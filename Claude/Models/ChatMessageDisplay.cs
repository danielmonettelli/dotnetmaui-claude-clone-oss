namespace Claude.Models;

/// <summary>
/// Represents a chat message for display in the UI
/// </summary>
public class ChatMessageDisplay
{
    /// <summary>
    /// The content of the message
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the message is from the user (true) or AI (false)
    /// </summary>
    public bool IsUser { get; set; }

    /// <summary>
    /// The timestamp when the message was sent/received
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Returns a formatted timestamp string for display
    /// </summary>
    public string FormattedTime => Timestamp.ToLocalTime().ToString("HH:mm:ss");
}
