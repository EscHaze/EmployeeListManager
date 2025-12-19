namespace EmployeeListManager.Domain;
public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsRemote { get; set; }
}
public interface IEmployeeRepository
{
    void Add(Employee employee);
    IReadOnlyList<Employee> GetAllEmployees();
    Employee? FindById(int id);
}
