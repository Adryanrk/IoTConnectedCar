using CarAnalyzer.Services;
using CarAnalyzer.Analysis;

string? readApiKey =
    Environment.GetEnvironmentVariable("THINGSPEAK_READ_API_KEY");

string? channelId =
    Environment.GetEnvironmentVariable("THINGSPEAK_CHANNEL_ID");

if (string.IsNullOrWhiteSpace(readApiKey) ||
    string.IsNullOrWhiteSpace(channelId))
{
    Console.WriteLine("ThingSpeak configuration is missing.");
    return;
}

ThingSpeakService thingSpeakService =
    new ThingSpeakService(readApiKey, channelId);

Console.WriteLine("ThingSpeak configuration loaded successfully.");
Console.WriteLine();

Console.WriteLine("IoT Connected Car Analyzer");
Console.WriteLine("==========================");
Console.WriteLine();

bool running = true;

while (running)
{
    Console.WriteLine("1. Analyze telemetry from the last 24 hours");
    Console.WriteLine("2. Analyze the last 100 data points");
    Console.WriteLine("0. Exit");
    Console.WriteLine();

    Console.Write("Select an option: ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Analyzing telemetry from the last 24 hours...");
            break;

        case "2":
            Console.WriteLine("Fetching the last 100 data points...");

            var telemetryList =
                await thingSpeakService.GetLast100EntriesAsync();

            Console.WriteLine($"Retrieved {telemetryList.Count} data points.");

            TelemetryAnalyzer.Analyze(telemetryList);
            break;

        case "0":
            running = false;
            Console.WriteLine("Exiting CarAnalyzer...");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

    Console.WriteLine();
}