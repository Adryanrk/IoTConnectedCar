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
    }
}