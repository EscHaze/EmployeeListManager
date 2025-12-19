using EmployeeListManager.Domain;
namespace EmployeeListManager.Application;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;
    public EmployeeService(IEmployeeRepository repository) => _repository = repository;
    public void AddEmployee(Employee employee)
    {
        _repository.Add(employee);
    }
    public IReadOnlyList<Employee> GetAllEmployees()
    {
        return _repository.GetAllEmployees();
    }
    public IReadOnlyList<Employee> GetRemoteEmployees()
    {
        return _repository.GetRemoteEmployees();
    }
    public Employee? FindById(int id)
    {
        return _repository.FindById(id);
    }
}
