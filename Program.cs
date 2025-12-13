using System.Globalization;
using Spectre.Console;
namespace AssistLabTask1;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsRemote { get; set; }
}

public class Program
{
    private static readonly List<Employee> employees = [];
    private static int _nextId = 1;
    static string StringValidator(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input!;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }
    static string? StringValidator(string message, string errorMessage, bool isNullAllowed)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (isNullAllowed && string.IsNullOrWhiteSpace(input))
                return null;
            else if (!string.IsNullOrWhiteSpace(input))
                return input!;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }
    static DateTime DateParser(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine()?.Trim();
            if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;
            else
                AnsiConsole.MarkupLine(errorMessage);
        }
    }
    static bool IsRemote(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            string? inputRemote = Console.ReadLine()?.Trim().ToLower();
            switch (inputRemote)
            {
                case "y" or "yes" or "1":
                    return true;
                case "n" or "no" or "0":
                    return false;
                default:
                    AnsiConsole.MarkupLine(errorMessage);
                    continue;
            }
        }
    }
    public static void AddEmployee()
    {
        var employee = new Employee();
        employee.FullName = StringValidator("Insert employee's full name: ", "[red]Full name can't be null![/]");
        employee.Position = StringValidator("Insert employee's position (optional): ", "[red]Position was specified, setting it to \"-\"[/]", true);
        employee.Position ??= "-";
        employee.HireDate = DateParser("Insert employee's hire date: ", "[red]Hire date is invalId![/]");
        employee.IsRemote = IsRemote("Is remote employee(y/n, yes/no, 1/0): ", "[red]InvalId input![/]");
        employee.Id = _nextId++;
        employees.Add(employee);
        AnsiConsole.MarkupLine($"[green]New employee {employee.FullName} is added successfully![/]");
    }
    static void ListAllEmployees()
    {
        AnsiConsole.MarkupLine("[yellow]Here is employee list: [/]");
        Table employeesTable = new();
        employeesTable.AddColumn("Id");
        employeesTable.AddColumn("Full name");
        employeesTable.AddColumn("Position");
        employeesTable.AddColumn("Hire date");
        employeesTable.AddColumn("Remote?");
        employeesTable.ShowRowSeparators();
        foreach (Employee e in employees)
        {
            employeesTable.AddRow(
                e.Id.ToString(),
                e.FullName,
                e.Position ?? "-",
                e.HireDate.ToString("dd.MM.yyyy"),
                e.IsRemote ? "Yes" : "No"
            );
        }
        AnsiConsole.Write(employeesTable);
    }
    static void ListRemoteEmployees()
    {
        AnsiConsole.MarkupLine("[yellow]Here is remote employee list: [/]");
        Table employeesTable = new();
        employeesTable.AddColumn("Id");
        employeesTable.AddColumn("Full name");
        employeesTable.AddColumn("Position");
        employeesTable.AddColumn("Hire date");
        employeesTable.ShowRowSeparators();
        List<Employee> remoteEmployees = [.. employees.Where(e => e.IsRemote)];
        foreach (Employee e in remoteEmployees)
        {
            employeesTable.AddRow(
                e.Id.ToString(),
                e.FullName,
                e.Position ?? "-",
                e.HireDate.ToString("dd.MM.yyyy")
            );
        }
        AnsiConsole.Write(employeesTable);
    }
    static void FindById()
    {
        Console.Write("Enter employee's Id: ");
        var idInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
        {
            AnsiConsole.MarkupLine("[red]Id must be a number![/]");
            return;
        }
        var employee = employees.Find(i => i.Id == id);
        if (employee is null)
        {
            AnsiConsole.MarkupLine($"[red]Employee with Id {id} not found.[/]");
            return;
        }
        AnsiConsole.MarkupLine($"[yellow]Here is employee with Id {id}: [/]");
        Table employeesTable = new();
        employeesTable.AddColumn("Id");
        employeesTable.AddColumn("Full name");
        employeesTable.AddColumn("Position");
        employeesTable.AddColumn("Hire date");
        employeesTable.AddColumn("Remote?");
        employeesTable.AddRow(
            employee.Id.ToString(),
            employee.FullName,
            employee.Position ?? "-",
            employee.HireDate.ToString("dd.MM.yyyy"),
            employee.IsRemote ? "Yes" : "No");
        AnsiConsole.Write(employeesTable);
    }
    static string GetInput()
    {
        while (true)
        {
            Console.WriteLine("\nWelcome, please select the action from the action list.");
            Console.WriteLine("Action list: \n1. Add a new employee \n2. List all employees \n3. List remote employees only \n4. Find by Id \n0. Exit the program.");
            Console.Write("Your input: ");
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
                    ListAllEmployees();
                    break;
                case "3":
                    ListRemoteEmployees();
                    break;
                case "4":
                    FindById();
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