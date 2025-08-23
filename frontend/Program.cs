using Avalonia;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using frontend.Models;
using frontend.ViewModels;
using frontend.Config;

namespace frontend;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Budowanie konfiguracji
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Tworzenie kontenera DI
        var services = new ServiceCollection();

        // Rejestracja ApiSettings jako singleton
        var apiSettings = new ApiSettings
        {
            BaseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value ?? "http://localhost:3000"
        };
        services.AddSingleton(apiSettings);

        // Rejestracja serwisów
        services.AddSingleton<PingService>();
        services.AddSingleton<RaceClient>();
        services.AddTransient<MainWindowViewModel>();

        // Build the service provider
        App.ServiceProvider = services.BuildServiceProvider();

        // Start the app
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
