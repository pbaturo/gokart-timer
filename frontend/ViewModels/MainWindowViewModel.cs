using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using frontend.Models;
using frontend.Config;
using System.Collections.ObjectModel;
using System.Linq;

namespace frontend.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly PingService _pingService;
        private readonly RaceClient _raceClient;

        [ObservableProperty]
        private string _greeting = "Welcome to Gokart Timer!";

        [ObservableProperty]
        private string _pingResult = "Not checked yet";

        [ObservableProperty]
        private bool _isServerConnected = false;

        [ObservableProperty]
        private ObservableCollection<LapViewModel> _laps = new();

        public MainWindowViewModel(PingService pingService, RaceClient raceClient)
        {
            _pingService = pingService;
            _raceClient = raceClient;
        }

        // Constructor for design-time
        public MainWindowViewModel() : this(
            new PingService(new Config.ApiSettings { BaseUrl = "http://design-time" }),
            new RaceClient(new Config.ApiSettings { BaseUrl = "http://design-time" }))
        {
            // Add some design-time data
            Laps.Add(new LapViewModel { DriverId = "Kierowca 1", LapNumber = 1, LapTime = "01:23.456" });
            Laps.Add(new LapViewModel { DriverId = "Kierowca 2", LapNumber = 1, LapTime = "01:22.789" });
        }

        [RelayCommand]
        private async Task CheckPing()
        {
            try
            {
                PingResult = "Checking...";
                string response = await _pingService.GetPingAsync();
                PingResult = $"Server time: {response}";
                IsServerConnected = true;
            }
            catch (Exception ex)
            {
                PingResult = $"Error: {ex.Message}";
                IsServerConnected = false;
            }
        }

        [RelayCommand]
        private async Task LoadLaps()
        {
            try
            {
                Laps.Clear();
                var lapData = await _raceClient.GetLapsAsync();
                
                foreach (var lap in lapData)
                {
                    Laps.Add(new LapViewModel
                    {
                        DriverId = $"Kierowca {lap.id}",  // Konwersja liczby na przyjazną nazwę
                        LapNumber = lap.lap,
                        LapTime = FormatTime(lap.time)
                    });
                }
            }
            catch (Exception ex)
            {
                // Można dodać obsługę błędów lub logowanie
                PingResult = $"Error loading laps: {ex.Message}";
                IsServerConnected = false;
            }
        }

        private string FormatTime(int timeInMs)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(timeInMs);
            return $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:000}";
        }
    }

    public class LapViewModel
    {
        public string DriverId { get; set; } = "";
        public int LapNumber { get; set; }
        public string LapTime { get; set; } = "";
    }
}
