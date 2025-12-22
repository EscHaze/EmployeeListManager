namespace EmployeeListManager.Domain;
public interface IEmployeeRepository
{
    void Add(Employee employee);
    IReadOnlyList<Employee> GetAllEmployees();
    Employee? FindById(int id);
    IReadOnlyList<Employee> GetRemoteEmployees();
}
