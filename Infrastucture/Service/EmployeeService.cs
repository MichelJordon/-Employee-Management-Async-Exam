using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DapperContext _context;
    public EmployeeService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Employee>>> GetEmployees()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, first_name as FirstName, last_name as LastName, email, phone_number as PhoneNumber, department_id as DepartmentId, manager_id as ManagerId, commission, salary, job_id as JobId, hire_date as HireDate from Employees";
            var response = await connection.QueryAsync<Employee>(sql);
            return new Response<List<Employee>>(response.ToList());
        }
    }
    public async Task<Response<Employee>> InsertEmployee(Employee employee)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into Employees(first_name, last_name, email,phone_number,department_id,manager_id,commission,salary,job_id,hire_date )" 
                + $"values ('{employee.FirstName}', '{employee.LastName}', '{employee.Email}', {employee.PhoneNumber}, {employee.DepartmentId},{employee.ManagerId},{employee.Commission},{employee.Salary},{employee.JobId},'{employee.HireDate}' ) returning id" ;
                var response = await connection.ExecuteScalarAsync<int>(sql);
                employee.Id = response;
                return new Response<Employee>(employee);
            }
        }
        catch (Exception ex)
        {
            return new Response<Employee>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Employee>> UpdateEmployee(Employee employee)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update Employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', email = '{employee.Email}', phone_number = {employee.PhoneNumber} , department_id = {employee.DepartmentId}, manager_id = {employee.ManagerId}, commission = {employee.Commission}, salary = {employee.Salary}, job_id = {employee.JobId}, hire_date = '{employee.HireDate}' where id = {employee.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                employee.Id = response;
                return new Response<Employee>(employee);
            }
        }
        catch (Exception ex)
        {
            return new Response<Employee>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteEmployee(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from Employees where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}