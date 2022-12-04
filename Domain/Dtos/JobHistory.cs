namespace Domain.Dto;
public class JobHistory
{
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int JobId { get; set; }
    public int DepartmentId { get; set; }

}