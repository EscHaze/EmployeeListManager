using Spectre.Console;
namespace AssistLabTask1;

public class Program
{
    static string GetInput()
    {
        while (true)
        {
            Console.WriteLine("\nWelcome, please select the action from the action list.");
            Console.WriteLine("Action list: 1. Add a new employee, 2. List all employees, 3. List remote employees only, 4. Find by ID, 0. Exit the program.");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                AnsiConsole.MarkupLine("[red]Can't be null, empty or white space, try again![/]");
                continue;
            }
            return input;
        }
    }
    static void Main()
    {
        while (true)
        {
            string input = GetInput();
            switch (input)
            {
                case "1":
                    // Add a new employee logic to be added... 
                    break;
                case "2":
                    // List all employees logic to be added...
                    break;
                case "3":
                    // List remote employees only logic to be added...
                    break;
                case "4":
                    // Find by ID logic to be added...
                    break;
                case "0":
                    AnsiConsole.Markup("[yellow]Exiting the program...[/]");
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Unlisted option. Please, try again.[/]");
                    break;
            }
        }
    }
}