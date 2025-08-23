using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using frontend.Models;

namespace frontend.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly PingService _pingService;

        [ObservableProperty]
        private string _greeting = "Welcome to Gokart Timer!";

        [ObservableProperty]
        private string _pingResult = "Not checked yet";

        // Konstruktor dla design-time
        public MainWindowViewModel() 
            : this(new PingService())
        {
        }

        // Konstruktor z dependency injection
        public MainWindowViewModel(PingService pingService)
        {
            _pingService = pingService;
        }

        [RelayCommand]
        private async Task CheckPingAsync()
        {
            try
            {
                PingResult = "Checking...";
                string response = await _pingService.GetPingAsync();
                PingResult = $"Server time: {response}";
            }
            catch (Exception ex)
            {
                PingResult = $"Error: {ex.Message}";
            }
        }
    }
}
