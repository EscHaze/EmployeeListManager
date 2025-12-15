using EmployeeListManager.Domain;
namespace EmployeeListManager.Application;

public class ApplicationClass
{
    private readonly IEmployeeRepository _repository;
    public ApplicationClass(IEmployeeRepository repository) => _repository = repository;
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
        return [.. _repository.GetAllEmployees().Where(e => e.IsRemote == true)];
    }
    public Employee? FindById(int id)
    {
        return _repository.FindById(id);
    }
}
