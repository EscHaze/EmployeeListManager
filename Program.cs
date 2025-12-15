using EmployeeListManager.Application;
using EmployeeListManager.Infrastructure;
using EmployeeListManager.ConsoleUi;
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