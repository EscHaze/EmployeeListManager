using EmployeeListManager.Application;
using EmployeeListManager.Infrastructure;
using EmployeeListManager.ConsoleUi;
namespace EmployeeListManager;

public class Program
{
    static void Main()
    {
        var repository = new InMemoryEmployeeRepository();
        var service = new EmployeeService(repository);
        var console = new EmployeeConsoleApp(service);
        console.Run();
    }
}