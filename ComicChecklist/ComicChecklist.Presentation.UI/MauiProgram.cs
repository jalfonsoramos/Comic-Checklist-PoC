using ComicChecklist.Presentation.UI.Services;
using ComicChecklist.Presentation.UI.ViewModels;
using ComicChecklist.Presentation.UI.Views;
using Microsoft.Extensions.Logging;

namespace ComicChecklist.Presentation.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddSingleton<SubscriptionsPage>();
            builder.Services.AddSingleton<ChecklistsPage>();
            builder.Services.AddSingleton<ChecklistDetailPage>();
            builder.Services.AddSingleton<UpdateIssuesPage>();

            builder.Services.AddSingleton<SubscriptionsViewModel>();
            builder.Services.AddSingleton<ChecklistsViewModel>();
            builder.Services.AddSingleton<ChecklistDetailsViewModel>();
            builder.Services.AddSingleton<UpdateIssuesViewModel>();

            builder.Services.AddSingleton<IChecklistApiService, ChecklistApiService>();

            builder.Services.AddHttpClient<IChecklistApiService, ChecklistApiService>(client =>
            {
                // HACK: In order to test the app in android or ios platforms is necessary to use dev tunnels.
                // Uncoment the following line (and comment the next) and set a valid url from dev tunnels.
                //client.BaseAddress = new Uri("https://k7r64xn3-5056.usw3.devtunnels.ms/");

                // HACK: Use this line to test the app in windows platform.
                client.BaseAddress = new Uri("https://localhost:7066/");
            });

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}