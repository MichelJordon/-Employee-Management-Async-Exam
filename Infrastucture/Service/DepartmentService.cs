using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DapperContext _context;
    public DepartmentService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Department>>> GetDepartments()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, department_name as DepartmentName, manager_id as ManagerId, location_id as LocationId  from departments;";
            var response = await connection.QueryAsync<Department>(sql);
            return new Response<List<Department>>(response.ToList());
        }
    }
    public async Task<Response<Department>> InsertDepartment(Department department)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into departments(department_name, manager_id, location_id) values ('{department.DepartmentName}', {department.ManagerId}, {department.LocationId} ) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                department.Id = response;
                return new Response<Department>(department);
            }
        }
        catch (Exception ex)
        {
            return new Response<Department>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Department>> UpdateDepartment(Department department)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update departments set department_name = '{department.DepartmentName}', manager_id = {department.ManagerId}, location_id = {department.LocationId}  where id = {department.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                department.Id = response;
                return new Response<Department>(department);
            }
        }
        catch (Exception ex)
        {
            return new Response<Department>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteDepartment(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from departments where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}