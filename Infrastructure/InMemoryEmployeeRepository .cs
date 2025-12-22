using EmployeeListManager.Domain;
namespace EmployeeListManager.Infrastructure;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = [];
    private int _nextId = 1;
    public void Add(Employee employee)
    {
        employee.Id = _nextId++;
        _employees.Add(employee);
    }
    public IReadOnlyList<Employee> GetAllEmployees()
    {
        return _employees;
    }
    public Employee? FindById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }
    public IReadOnlyList<Employee> GetRemoteEmployees()
    {
        return [.._employees.Where(e => e.IsRemote)];
    }
}
