using CarSimulator;

string? writeApiKey =
    Environment.GetEnvironmentVariable("THINGSPEAK_WRITE_API_KEY");

if (string.IsNullOrWhiteSpace(writeApiKey))
{
    Console.WriteLine("ThingSpeak Write API Key was not found.");
    return;
}

Console.WriteLine("ThingSpeak configuration loaded successfully.");
Console.WriteLine();

Car car = new Car();

Console.WriteLine("IoT Connected Car Simulator");
Console.WriteLine("==========================");
Console.WriteLine();

for (int i = 0; i < 25; i++)
{
    var telemetry = car.GenerateTelemetry();

    Console.WriteLine($"Speed:              {telemetry.Speed:F1} km/h");
    Console.WriteLine($"RPM:                {telemetry.Rpm:F0}");
    Console.WriteLine($"Fuel Level:         {telemetry.FuelLevel:F2} %");
    Console.WriteLine($"Engine Temperature: {telemetry.EngineTemperature:F1} °C");
    Console.WriteLine("------------------------------");

    Thread.Sleep(1000);
}