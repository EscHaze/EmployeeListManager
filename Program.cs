using System.Globalization;
using Spectre.Console;
namespace AssistLabTask1;

public class Employee
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsRemote { get; set; }
}


public class Program
{
    private static readonly List<Employee> employees = [
new Employee
    {
        Id = 1,
        FullName = "Jake Peralta",
        Position = "HR",
        HireDate = new DateTime(2004, 12, 21),
        IsRemote = true
    },
    new Employee
    {
        Id = 2,
        FullName = "Sam Smith",
        Position = "Developer",
        HireDate = new DateTime(2025, 5, 12),
        IsRemote = false
    },
    new Employee
    {
        Id = 3,
        FullName = "Anna Johnson",
        Position = "Accountant",
        HireDate = new DateTime(2020, 3, 1),
        IsRemote = true
    }
    ];
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
            Console.Write("Insert employee's position (optional): ");
            string? positionInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(positionInput))
            {
                AnsiConsole.MarkupLine("[red]Position was specified, setting it to \"-\"[/]");
                employee.Position = "-";
            }
            else
            {
                employee.Position = positionInput;
            }
            Console.Write("Insert employee's hire date: ");
            string? dateInput = Console.ReadLine();
            if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                employee.HireDate = result;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Hire date is invalId![/]");
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
                    AnsiConsole.MarkupLine("[red]InvalId input![/]");
                    continue;
            }
            employee.Id = employees.Count + 1;
            employees.Add(employee);
            AnsiConsole.MarkupLine($"[green]New employee {employee.FullName} is added successfully![/]");
            break;
        }
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
                e.Position,
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
        List<Employee> remoteEmployees = [.. employees.Where(e => e.IsRemote == true)];
        foreach (Employee e in remoteEmployees)
        {
            employeesTable.AddRow(
                e.Id.ToString(),
                e.FullName,
                e.Position,
                e.HireDate.ToString("dd.MM.yyyy")
            );
        }
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
                    // Find by Id logic to be added...
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