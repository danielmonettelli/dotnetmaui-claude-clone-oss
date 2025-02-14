namespace Claude.Services;

public interface IAnthropicService
{
    Task<string> SendChatMessageAsync(string query);
}
