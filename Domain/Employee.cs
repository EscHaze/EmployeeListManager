namespace EmployeeListManager.Domain;
public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsRemote { get; set; }
    private Employee(string fullName, string position, DateTime hireDate, bool isRemote)
    {
        FullName = fullName;
        Position = position;
        HireDate = hireDate;
        IsRemote = isRemote;
    }
    public static Employee Create(string fullName, string? position, DateTime hireDate, bool isRemote)
    {
        var finalPosition = string.IsNullOrWhiteSpace(position) ? "-" : position;
        return new Employee(fullName, finalPosition, hireDate, isRemote);
    }
}