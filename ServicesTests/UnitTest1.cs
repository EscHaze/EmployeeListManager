using EmployeeListManager.Application;
using EmployeeListManager.Domain;
using Moq;

namespace EmployeeListManager.ApplicationTests;

public class UnitTest1
{
    [Fact]
    public void AddEmployee_ShouldCallRepositoryAddExactlyOnce()
    {
        var newEmployee = new Employee
        {
            Id = 1,
            FullName = "David Martinez",
            HireDate = new DateTime(2077, 10, 25),
            Position = "PR",
            IsRemote = true
        };
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
            new () { Id = 1, FullName = "Walter White", HireDate = new DateTime(2008, 03, 27),IsRemote = true },
            new () { Id = 2, FullName = "Jesse Pinkman", HireDate = new DateTime(2008, 06, 02),IsRemote = false },
            new () { Id = 3, FullName = "Gus Fring", HireDate = new DateTime(1995, 05, 12),  IsRemote = true},
            new () { Id = 4, FullName = "Mike Ehrmantraut", HireDate = new DateTime(2002, 12, 20),IsRemote = false }
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
            new () { Id = 1, FullName = "Walter White", HireDate = new DateTime(2008, 03, 27),IsRemote = true },
            new () { Id = 2, FullName = "Jesse Pinkman", HireDate = new DateTime(2008, 06, 02),IsRemote = false },
            new () { Id = 3, FullName = "Gus Fring", HireDate = new DateTime(1995, 05, 12),  IsRemote = true},
            new () { Id = 4, FullName = "Mike Ehrmantraut", HireDate = new DateTime(2002, 12, 20),IsRemote = false }
        };
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.GetAllEmployees()).Returns(employees);
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
        var excpectedEmployee = new Employee { Id = targetId, FullName = "Mike Ehrmantraut", HireDate = new DateTime(2002, 12, 20), IsRemote = false };
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