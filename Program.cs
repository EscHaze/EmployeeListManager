using EmployeeListManager.Application;
using EmployeeListManager.ConsoleUi;
using EmployeeListManager.Infrastructure;
namespace EmployeeListManager;

public class Program
{
    static void Main()
    {
        var repository = new InfrastructureClass();
        var application = new ApplicationClass(repository);
        var console = new ConsoleUiClass(application);
        console.Run();
    }
}