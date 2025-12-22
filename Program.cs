using EmployeeListManager.Services;
using EmployeeListManager.EmployeeConsoleApp;
using EmployeeListManager.Infrastructure;
namespace EmployeeListManager;

public class Program
{
    static void Main()
    {
        try
        {
            var repository = new InMemoryEmployeeRepository();
            var service = new EmployeeService(repository);
            var console = new EmployeeConsoleAppClass(service);
            console.Run();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An unexpected error occurred. The application will now close.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}