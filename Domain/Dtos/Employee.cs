namespace Domain.Dto;
public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public int DepartmentId  { get; set; }
    public int ManagerId  { get; set; }
    public int Commission { get; set; }
    public double Salary { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
}