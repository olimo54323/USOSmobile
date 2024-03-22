using Microsoft.Extensions.Logging;
using USOSmobile.Models;
using USOSmobile.SubPages;

namespace USOSmobile.Pages
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<APIBrowser>();
            builder.Services.AddSingleton<User>();
            builder.Services.AddSingleton<PinPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<MyUSOSPage>();
            builder.Services.AddSingleton<SchedulePage>();
            builder.Services.AddSingleton<ExamsPage>();
            builder.Services.AddSingleton<GradesPage>();
            builder.Services.AddSingleton<ActivityGroupsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
