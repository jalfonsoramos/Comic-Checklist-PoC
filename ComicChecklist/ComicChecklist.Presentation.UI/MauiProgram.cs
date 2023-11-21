﻿using ComicChecklist.Presentation.UI.Services;
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
            builder.Services.AddSingleton<SubscriptionsViewModel>();
            builder.Services.AddTransient<ChecklistsPage>();
            builder.Services.AddTransient<ChecklistsViewModel>();

            builder.Services.AddScoped<IChecklistApiService, ChecklistApiService>();

            builder.Services.AddHttpClient<IChecklistApiService, ChecklistApiService>(client =>
            {
                client.BaseAddress = new Uri("https://k7r64xn3-5056.usw3.devtunnels.ms/");
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