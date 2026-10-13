namespace CarAnalyzer.Models
{
    public class TelemetryData
    {
        public DateTime CreatedAt { get; set; }

        public double? Speed { get; set; }

        public double? Rpm { get; set; }

        public double? FuelLevel { get; set; }

        public double? EngineTemperature { get; set; }
    }
}