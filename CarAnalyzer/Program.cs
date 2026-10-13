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

    switch (choice)
    {
        case "1":
            Console.WriteLine("Analyzing telemetry from the last 24 hours...");
            break;

        case "2":
            Console.WriteLine("Analyzing the last 100 data points...");
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