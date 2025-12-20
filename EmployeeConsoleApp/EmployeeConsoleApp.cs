using System.Globalization;
using EmployeeListManager.Application;
using Spectre.Console;
using EmployeeListManager.Domain;
namespace EmployeeListManager.ConsoleUi;

public class EmployeeConsoleApp
{
    private readonly EmployeeService _application;
    public EmployeeConsoleApp(EmployeeService application)
    {
        _application = application;
    }
    public void Run()
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
                    FindEmployeesById();
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
    /// <summary>
    /// Reads a line from the console and repeats until the user enters a non‑empty, non‑whitespace string.
    /// </summary>
    /// <param name="message">Console message with information for user</param>
    /// <param name="errorMessage">Validation error message</param>
    /// <returns>Returns non-null string values</returns>
    public string StringValidator(string message, string errorMessage)
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
    /// <summary>
    /// Overload of StringValidator method, allows using null values if user needs default value to be assigned
    /// </summary>
    /// <param name="message">Console message with information for user</param>
    /// <param name="errorMessage">Validation error message</param>
    /// <param name="isNullAllowed">Permission to use null value</param>
    /// <returns>Returns non-null string value if !isNullAllowed, returns null value if isNullAllowed and input is null, empty or white space</returns>
    public string? StringValidator(string message, bool isNullAllowed)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (isNullAllowed && string.IsNullOrWhiteSpace(input))
            {
                AnsiConsole.MarkupLine("[grey]Input skipped. Default value will be used.[/]");
                return null;
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                return input!;
            }
        }
    }
    /// <summary>
    /// Checks format of the string. Required format is "dd.MM.yyyy"
    /// </summary>
    /// <param name="message">Console message with information for user</param>
    /// <param name="errorMessage">Validation error message</param>
    public DateTime DateParser(string message, string errorMessage)
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
    /// <summary>
    /// Checks if employee's status remote or not
    /// </summary>
    /// <param name="message">Console message with information for user</param>
    /// <param name="errorMessage">Validation error message</param>
    public bool IsRemote(string message, string errorMessage)
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
    public void AddEmployee()
    {
        var employee = Employee.Create(
                    StringValidator("Insert employee's full name: ", "[red]Full name can't be null![/]"),
                    StringValidator("Insert employee's position (optional): ", true),
                    DateParser("Insert employee's hire date: ", "[red]Hire date is invalId![/]"),
                    IsRemote("Is remote employee(y/n, yes/no, 1/0): ", "[red]InvalId input![/]")
        );
        _application.AddEmployee(employee);
        AnsiConsole.MarkupLine($"[green]New employee {employee.FullName} is added successfully![/]");
    }
    public void ListAllEmployees()
    {
        AnsiConsole.MarkupLine("[yellow]Here is employee list: [/]");
        Table employeesTable = new();
        employeesTable.AddColumn("Id");
        employeesTable.AddColumn("Full name");
        employeesTable.AddColumn("Position");
        employeesTable.AddColumn("Hire date");
        employeesTable.AddColumn("Remote?");
        employeesTable.ShowRowSeparators();
        foreach (Employee e in _application.GetAllEmployees())
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
    public void ListRemoteEmployees()
    {
        AnsiConsole.MarkupLine("[yellow]Here is remote employee list: [/]");
        Table employeesTable = new();
        employeesTable.AddColumn("Id");
        employeesTable.AddColumn("Full name");
        employeesTable.AddColumn("Position");
        employeesTable.AddColumn("Hire date");
        employeesTable.ShowRowSeparators();
        foreach (Employee e in _application.GetRemoteEmployees())
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
    public void FindEmployeesById()
    {
        Console.Write("Enter employee's Id: ");
        var idInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
        {
            AnsiConsole.MarkupLine("[red]Id must be a number![/]");
            return;
        }
        var employee = _application.FindById(id);
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
    public string GetInput()
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
}