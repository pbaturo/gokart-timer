using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using frontend.ViewModels;
using frontend.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace frontend;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            // Try to get the ViewModel from DI
            MainWindowViewModel? viewModel = null;
            
            if (ServiceProvider != null)
            {
                viewModel = ServiceProvider.GetService<MainWindowViewModel>();
            }
            
            // If DI failed or ServiceProvider is null, create a temporary view model with default services
            if (viewModel == null)
            {
                // Create basic services for the fallback view model
                var apiSettings = new Config.ApiSettings { BaseUrl = "http://localhost:3000" };
                var pingService = new Models.PingService(apiSettings);
                var raceClient = new Models.RaceClient(apiSettings);
                viewModel = new MainWindowViewModel(pingService, raceClient);
            }
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}