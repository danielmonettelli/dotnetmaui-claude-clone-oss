using Claude.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Claude.Views;

public partial class ChatPage : ContentPage
{
    private ChatViewModel _viewModel;

    // Constructor sin parámetros necesario para Shell en Android
    public ChatPage()
    {
        try
        {
            InitializeComponent();
            
            // Intentar obtener el ViewModel desde el contenedor de servicios
            if (Application.Current?.Handler?.MauiContext != null)
            {
                _viewModel = Application.Current.Handler.MauiContext.Services.GetService<ChatViewModel>();
                
                // Si no se pudo obtener el ViewModel, crear uno de emergencia
                if (_viewModel == null)
                {
                    var service = new Claude.Services.SimulatedAnthropicService();
                    _viewModel = new ChatViewModel(service);
                    _viewModel.Title = "Claude (Modo Emergencia)";
                }
                
                BindingContext = _viewModel;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al inicializar ChatPage: {ex.Message}");
        }
    }
    
    // Constructor con inyección de dependencias para uso normal
    public ChatPage(ChatViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al inicializar ChatPage con ViewModel: {ex.Message}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        try
        {
            // Verificar si _viewModel no es null para evitar errores
            if (_viewModel?.Title != null && (_viewModel.Title.Contains("Demo") || _viewModel.Title.Contains("Emergencia")))
            {
                // Modo simulación o emergencia
                Shell.Current.BackgroundColor = Colors.OrangeRed;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en OnAppearing: {ex.Message}");
        }
    }
}