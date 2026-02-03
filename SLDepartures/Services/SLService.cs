using System.Text.Json;
using SLDepartures.Models;

namespace SLDepartures.Services;

public class SLService
{
    private readonly HttpClient _httpClient;

    public SLService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Departure>> GetDepartures(string siteId = "9116")
    {
        var url = $"https://transport.integration.sl.se/v1/sites/{siteId}/departures?forecast=30";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var document = JsonDocument.Parse(json);

        var departures = new List<Departure>();
        var departuresArray = document.RootElement.GetProperty("departures");

        foreach (var dep in departuresArray.EnumerateArray().Take(10))
        {
            departures.Add(new Departure
            {
                Linje = dep.GetProperty("line").GetProperty("designation").GetString() ?? "",
                Destination = dep.GetProperty("destination").GetString() ?? "",
                Visar = dep.GetProperty("display").GetString() ?? "",
                Typ = dep.GetProperty("line").GetProperty("transport_mode").GetString() ?? "",
                Status = dep.GetProperty("state").GetString() ?? ""
            });
        }

        return departures;
    }
}