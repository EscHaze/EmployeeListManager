using EmployeeListManager.Services;
using EmployeeListManager.Domain;
using Moq;

namespace EmployeeListManager.ApplicationTests;

public class UnitTest1
{
    [Fact]
    public void AddEmployee_ShouldCallRepositoryAddExactlyOnce()
    {
        var newEmployee = Employee.Create
        (
            "David Martinez",
            "PR",
            new DateTime(2077, 10, 25),
            true
        );
        var mockRepo = new Mock<IEmployeeRepository>();
        var application = new EmployeeService(mockRepo.Object);
        application.AddEmployee(newEmployee);
        mockRepo.Verify(r => r.Add(newEmployee), Times.Once);
    }
    [Fact]
    public void GetAllEmployees_ShouldReturnAllEmployeesFromRepository()
    {
        var employees = new List<Employee>
        {
            Employee.Create("Walter White", null, new DateTime(2008, 03, 27), true),
            Employee.Create("Jesse Pinkman", null, new DateTime(2008, 06, 02), false),
            Employee.Create("Gus Fring", null, new DateTime(1995, 05, 12), true),
            Employee.Create("Mike Ehrmantraut", null, new DateTime(2002, 12, 20), false)
        };
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.GetAllEmployees()).Returns(employees);
        var application = new EmployeeService(mockRepo.Object);
        var result = application.GetAllEmployees();
        Assert.Equal(4, result.Count);
        Assert.Contains(result, e => e.FullName == "Walter White");
        Assert.Contains(result, e => e.FullName == "Jesse Pinkman");
        Assert.Contains(result, e => e.FullName == "Gus Fring");
        Assert.Contains(result, e => e.FullName == "Mike Ehrmantraut");
    }
    [Fact]
    public void GetRemoteEmployees_ShouldReturnAllRemoteEmployeesFromRepository()
    {
        var employees = new List<Employee>
        {
            Employee.Create("Walter White", null, new DateTime(2008, 03, 27), true),
            Employee.Create("Jesse Pinkman", null, new DateTime(2008, 06, 02), false),
            Employee.Create("Gus Fring", null, new DateTime(1995, 05, 12), true),
            Employee.Create("Mike Ehrmantraut", null, new DateTime(2002, 12, 20), false)
        };
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.GetRemoteEmployees()).Returns([..employees.Where(e=>e.IsRemote)]);
        var application = new EmployeeService(mockRepo.Object);
        var remoteEmployees = application.GetRemoteEmployees();
        Assert.Equal(2, remoteEmployees.Count);
        Assert.Contains(remoteEmployees, r => r.FullName == "Walter White");
        Assert.Contains(remoteEmployees, r => r.FullName == "Gus Fring");
    }
    [Fact]
    public void FindById_WhenEmployeeExists_ShouldReturnEmployeeWithSelectedId()
    {
        const int targetId = 23;
        var excpectedEmployee = Employee.Create("Mike Ehrmantraut", null, new DateTime(2002, 12, 20), false);
        excpectedEmployee.Id = targetId;
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.FindById(targetId)).Returns(excpectedEmployee);
        var application = new EmployeeService(mockRepo.Object);
        var result = application.FindById(targetId);
        Assert.NotNull(result);
        Assert.Equal(targetId, result.Id);
    }
    [Fact]
    public void FindById_WhenEmployeeDoesNotExist_ShouldReturnNull()
    {
        const int nonExistentId = 2000;
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.FindById(It.IsAny<int>())).Returns((Employee)null);
        var application = new EmployeeService(mockRepo.Object);
        var result = application.FindById(nonExistentId);
        Assert.Null(result);
    }
}