using System.Globalization;
using CarSimulator.Models;

namespace CarSimulator.Services
{
    public class ThingSpeakService
    {
        private readonly HttpClient _httpClient;
        private readonly string _writeApiKey;

        public ThingSpeakService(string writeApiKey)
        {
            _httpClient = new HttpClient();
            _writeApiKey = writeApiKey;
        }

        public async Task<bool> SendTelemetryAsync(TelemetryData telemetry)
        {
            string speed = telemetry.Speed.ToString(
                CultureInfo.InvariantCulture);

            string rpm = telemetry.Rpm.ToString(
                CultureInfo.InvariantCulture);

            string fuelLevel = telemetry.FuelLevel.ToString(
                CultureInfo.InvariantCulture);

            string engineTemperature = telemetry.EngineTemperature.ToString(
                CultureInfo.InvariantCulture);

            string url =
                $"https://api.thingspeak.com/update" +
                $"?api_key={_writeApiKey}" +
                $"&field1={speed}" +
                $"&field2={rpm}" +
                $"&field3={fuelLevel}" +
                $"&field4={engineTemperature}";

            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                string result = await response.Content.ReadAsStringAsync();

                return int.TryParse(result.Trim(), out int entryId) && entryId > 0;
            }

            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return false;
            }

            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Request timed out or was canceled: {ex.Message}");
                return false;
            }
        }
    }
}