using CarAnalyzer.Models;

namespace CarAnalyzer.Analysis
{
    public class TelemetryAnalyzer
    {
        public static void Analyze(List<TelemetryData> telemetryList)
        {
            if (telemetryList.Count == 0)
            {
                Console.WriteLine("No telemetry data available.");
                return;
            }

            var speedValues = telemetryList
                .Where(t => t.Speed.HasValue)
                .Select(t => t.Speed!.Value)
                .ToList();

            var rpmValues = telemetryList
                .Where(t => t.Rpm.HasValue)
                .Select(t => t.Rpm!.Value)
                .ToList();

            if (speedValues.Count == 0 || rpmValues.Count == 0)
            {
                Console.WriteLine("Not enough valid telemetry data for analysis.");
                return;
            }

            double averageSpeed = speedValues.Average();
            double averageRpm = rpmValues.Average();

            Console.WriteLine();
            Console.WriteLine("Telemetry Analysis Results");
            Console.WriteLine("==========================");
            Console.WriteLine($"Total data points: {telemetryList.Count}");
            Console.WriteLine($"Average Speed:     {averageSpeed:F2} km/h");
            Console.WriteLine($"Average RPM:       {averageRpm:F0}");
        }
    }
}