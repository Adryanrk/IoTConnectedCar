using CarSimulator;
using CarSimulator.Services;

string? writeApiKey =
    Environment.GetEnvironmentVariable("THINGSPEAK_WRITE_API_KEY");

if (string.IsNullOrWhiteSpace(writeApiKey))
{
    Console.WriteLine("ThingSpeak Write API Key was not found.");
    return;
}

Console.WriteLine("ThingSpeak configuration loaded successfully.");
Console.WriteLine();

ThingSpeakService thingSpeakService =
    new ThingSpeakService(writeApiKey);

Car car = new Car();

Console.WriteLine("IoT Connected Car Simulator");
Console.WriteLine("==========================");
Console.WriteLine();

var telemetry = car.GenerateTelemetry();

Console.WriteLine($"Speed:              {telemetry.Speed:F1} km/h");
Console.WriteLine($"RPM:                {telemetry.Rpm:F0}");
Console.WriteLine($"Fuel Level:         {telemetry.FuelLevel:F2} %");
Console.WriteLine($"Engine Temperature: {telemetry.EngineTemperature:F1} °C");
Console.WriteLine();

bool success =
    await thingSpeakService.SendTelemetryAsync(telemetry);

if (success)
{
    Console.WriteLine("Telemetry sent successfully to ThingSpeak.");
}
else
{
    Console.WriteLine("Failed to send telemetry to ThingSpeak.");
}