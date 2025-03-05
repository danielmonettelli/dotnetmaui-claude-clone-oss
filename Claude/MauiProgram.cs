namespace Claude;

public static class MauiProgram
{
    // Bandera para habilitar el modo de simulación
    private static readonly bool UseSimulation = string.IsNullOrEmpty(Constants.ApiConstants.ANTHROPIC_API_KEY) || Constants.ApiConstants.ANTHROPIC_API_KEY == "CLAUDE_API_KEY_HERE" || Constants.ApiConstants.ANTHROPIC_API_KEY == "your_api_key_here";
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("Poppins-Regular.ttf", "Poppins#400");
            fonts.AddFont("Poppins-SemiBold.ttf", "Poppins#600");
            fonts.AddFont("NimbusRomNo9L-Reg.ttf", "NimbusRomanNo9L#400");
            fonts.AddFont("NimbusRomNo9L-Bold.ttf", "NimbusRomanNo9L#700");
        }).UseMauiCommunityToolkit();

        // Register HttpClient
        builder.Services.AddHttpClient();
        // Register factory and services
        builder.Services.AddSingleton<AnthropicServiceFactory>();
        // Registrar el servicio adecuado según la configuración
        builder.Services.AddSingleton<IAnthropicService>(sp =>
        {
            if (UseSimulation)
            {
                return new SimulatedAnthropicService();
            }
            else
            {
                IHttpClientFactory httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                HttpClient httpClient = httpClientFactory.CreateClient();
                return new AnthropicService(httpClient);
            }
        });
        // Register ViewModels with appropriate title based on simulation mode
        builder.Services.AddSingleton<ChatViewModel>(sp =>
        {
            IAnthropicService service = sp.GetRequiredService<IAnthropicService>();
            ChatViewModel viewModel = new(service);
            // Cambiar el título si estamos en modo simulación
            if (UseSimulation)
            {
                viewModel.Title = "Claude (Modo Demo)";
            }

            return viewModel;
        });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}