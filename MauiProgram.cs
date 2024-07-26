using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using UltimateMatch.Resources.Utils;

namespace UltimateMatch
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            // Cambiar el tamaño de la pantalla
            Task.Run(async () => await PantallaConfig.SetDimensionesPantallaAsync(1900, 900));

            return app;
            //return builder.Build();
        }
    }
}