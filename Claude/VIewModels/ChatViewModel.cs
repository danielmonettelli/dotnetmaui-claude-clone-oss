using Claude.Models;
using Claude.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Claude.ViewModels;

public partial class ChatViewModel : BaseViewModel
{
    private readonly IAnthropicService _anthropicService;

    [ObservableProperty]
    private string _userMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ChatMessageDisplay> _messages;

    public ChatViewModel(IAnthropicService anthropicService)
    {
        _anthropicService = anthropicService;
        _messages = new ObservableCollection<ChatMessageDisplay>();
        Title = "Chat with Claude";
    }

    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(UserMessage))
            return;

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            ClearError();

            // Add user message to the chat
            var userMessageDisplay = new ChatMessageDisplay
            {
                Content = UserMessage,
                IsUser = true,
                Timestamp = DateTime.Now
            };
            Messages.Add(userMessageDisplay);

            // Clear input field
            var messageToSend = UserMessage;
            UserMessage = string.Empty;

            // Get AI response
            var response = await _anthropicService.SendChatMessageAsync(messageToSend);

            // Add AI response to the chat
            var aiMessageDisplay = new ChatMessageDisplay
            {
                Content = response,
                IsUser = false,
                Timestamp = DateTime.Now
            };
            Messages.Add(aiMessageDisplay);
        }
        catch (AnthropicException ex)
        {
            ShowError($"API Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            ShowError($"An error occurred: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void ClearChat()
    {
        Messages.Clear();
        ClearError();
    }
}
