namespace Claude.ViewModels;

/// <summary>
/// Base ViewModel class that provides common functionality for all ViewModels
/// </summary>
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isError;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    protected void ShowError(string message)
    {
        ErrorMessage = message;
        IsError = true;
    }

    protected void ClearError()
    {
        ErrorMessage = string.Empty;
        IsError = false;
    }
}
