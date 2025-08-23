using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using frontend.Config;

namespace frontend.Models
{
    public sealed class LapDto
    {
        // id = identyfikator kierowcy (np. "1")
        public int id { get; set; }
        // time = czas okrążenia w ms
        public int time { get; set; }
        // lap = numer okrążenia (1..n)
        public int lap { get; set; }
    }
    
    public class RaceClient
    {
        private readonly HttpClient _http;
        // Add the missing JSON options
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        };
        
        public RaceClient(ApiSettings settings, HttpClient? client = null)
        {
            _http = client ?? new HttpClient
            {
                BaseAddress = new Uri(settings.BaseUrl)
            };
        }

        /// Pobiera wszystkie okrążenia bieżącego wyścigu
        public async Task<List<LapDto>> GetLapsAsync()
        {
            try
            {
                var response = await _http.GetAsync("/times");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("GetLapsAsync: response received {0}", json);
                return JsonSerializer.Deserialize<List<LapDto>>(json, _jsonOptions) ?? new List<LapDto>();
            }
            catch (Exception ex)
            {
                // Można dodać logowanie
                Console.WriteLine($"Błąd podczas pobierania okrążeń: {ex.Message}");
                return new List<LapDto>();
            }
        }
    }
}
