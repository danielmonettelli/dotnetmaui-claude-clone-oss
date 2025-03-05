namespace Claude.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatViewModel _viewModel;
    public ChatPage(ChatViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}