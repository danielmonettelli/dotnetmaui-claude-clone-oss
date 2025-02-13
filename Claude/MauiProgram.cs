﻿using Microsoft.Extensions.Logging;

namespace Claude
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins#400");
                    fonts.AddFont("Poppins-SemiBold.ttf", "Poppins#600");
                    fonts.AddFont("NimbusRomNo9L-Reg.ttf", "NimbusRomanNo9L#400");
                    fonts.AddFont("NimbusRomNo9L-Bold.ttf", "NimbusRomanNo9L#700");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
