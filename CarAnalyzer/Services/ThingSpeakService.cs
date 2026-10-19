using System.Text.Json;
using CarAnalyzer.Models;

namespace CarAnalyzer.Services
{
    public class ThingSpeakService
    {
        private readonly HttpClient _httpClient;
        private readonly string _readApiKey;
        private readonly string _channelId;

        public ThingSpeakService(string readApiKey, string channelId)
        {
            _httpClient = new HttpClient();
            _readApiKey = readApiKey;
            _channelId = channelId;
        }

        public async Task<List<TelemetryData>> GetLast100EntriesAsync()
        {
            string url =
                $"https://api.thingspeak.com/channels/{_channelId}/feeds.json" +
                $"?api_key={_readApiKey}&results=100";

            return await GetTelemetryAsync(url);
        }

        public async Task<List<TelemetryData>> GetLast24HoursAsync()
        {
            string url =
                $"https://api.thingspeak.com/channels/{_channelId}/feeds.json" +
                $"?api_key={_readApiKey}&days=1";

            return await GetTelemetryAsync(url);
        }

        private async Task<List<TelemetryData>> GetTelemetryAsync(string url)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string json =
                    await response.Content.ReadAsStringAsync();

                using JsonDocument document = JsonDocument.Parse(json);

                JsonElement feeds = document.RootElement.GetProperty("feeds");

                List<TelemetryData> telemetryList = new List<TelemetryData>();

                foreach (JsonElement feed in feeds.EnumerateArray())
                {
                    TelemetryData telemetry = new TelemetryData
                    {
                        CreatedAt = feed.GetProperty("created_at").GetDateTime(),
                        Speed = GetNullableDouble(feed, "field1"),
                        Rpm = GetNullableDouble(feed, "field2"),
                        FuelLevel = GetNullableDouble(feed, "field3"),
                        EngineTemperature = GetNullableDouble(feed, "field4")
                    };

                    telemetryList.Add(telemetry);
                }

                return telemetryList;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return new List<TelemetryData>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON error: {ex.Message}");
                return new List<TelemetryData>();
            }
        }

        private static double? GetNullableDouble(JsonElement feed, string fieldName)
        {
            if (feed.TryGetProperty(fieldName, out JsonElement value) &&
                value.ValueKind == JsonValueKind.String &&
                double.TryParse(
                    value.GetString(),
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double result))
            {
                return result;
            }

            return null;
        }
    }
}