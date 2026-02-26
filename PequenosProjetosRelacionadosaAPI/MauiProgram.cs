using Microsoft.Extensions.Logging;
using PequenosProjetosRelacionadosaAPI;
using PequenosProjetosRelacionadosaAPI.Services;
using System;

namespace PequenosProjetosRelacionadosaAPI
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

            // Serviços das APIs externas
            builder.Services.AddSingleton<IPokemonServices, PokemonService>();
            builder.Services.AddSingleton<IRAWGService, RAWGService>();

            // Serviços das APIs próprias com HttpClient configurado com as portas CORRETAS
            builder.Services.AddSingleton<IQuizService>(sp =>
            {
                var client = new HttpClient { BaseAddress = new Uri("https://localhost:7195") }; // porta HTTP do QuizAPI
                return new QuizService(client);
            });

            builder.Services.AddSingleton<IJokeService>(sp =>
            {
                var client = new HttpClient { BaseAddress = new Uri("https://localhost:7052") }; // porta HTTP do JokesAPI
                return new JokeService(client);
            });

            builder.Services.AddSingleton<ICurrencyService>(sp =>
            {
                var client = new HttpClient { BaseAddress = new Uri("https://localhost:7281") }; // porta HTTP do CurrencyAPI
                return new CurrencyService(client);
            });

            // HttpClient genérico (opcional)
            builder.Services.AddSingleton<HttpClient>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}