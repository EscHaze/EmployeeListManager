using System.Globalization;
using Spectre.Console;
namespace AssistLabTask1;

public class Employee
{
    public int ID { get; set; }
    public required string FullName { get; set; }
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsRemote { get; set; }
}


public class Program
{
    private static readonly List<Employee> employees = [];
    private static readonly Table employeesTable = new();
    public static void AddEmployee()
    {
        while (true)
        {
            Console.Write("Insert employee's full name: ");
            string? nameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                AnsiConsole.MarkupLine("[red]Full name can't be null![/]");
                continue;
            }
            var employee = new Employee() { FullName = nameInput };
            Console.Write("Insert employee's hire date: ");
            string? positionInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                AnsiConsole.MarkupLine("[red]Position was specified, setting it to \"-\"[/]");
                
            }
            string? dateInput = Console.ReadLine();
            if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                employee.HireDate = result;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Hire date is invalid![/]");
                continue;
            }
            Console.Write("Is remote employee(y/n, yes/no, 1/0): ");
            string? isRemote = Console.ReadLine()?.Trim().ToLower();
            switch (isRemote)
            {
                case "y" or "yes" or "1":
                    employee.IsRemote = true;
                    break;
                case "n" or "no" or "0":
                    employee.IsRemote = false;
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid input![/]");
                    continue;
            }
            employee.ID = employees.Count + 1;
            employees.Add(employee);
            AnsiConsole.MarkupLine($"[green]New employee {employee.FullName} is added successfully![/]");
            break;
        }
    }
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
                    AddEmployee();
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