namespace Domain.Dto;
public class Department
{
    public  int Id { get; set; }
    public  string DepartmentName { get; set; }
    public int ManagerId { get; set; }
    public  int LocationId { get; set; }
   
}