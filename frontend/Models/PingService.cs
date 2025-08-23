using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Models;

public class PingService
{
    private readonly HttpClient _client;

    public PingService(HttpClient? client = null)
    {
        _client = client ?? new HttpClient
        {
            BaseAddress = new Uri("http://192.168.1.101:3000")
        };
    }

    public async Task<string> GetPingAsync()
    {
        var response = await _client.GetAsync("/ping");
        response.EnsureSuccessStatusCode();

        // The endpoint returns plain text, so read it directly
        var text = await response.Content.ReadAsStringAsync();
        return text.Trim();
    }
}