using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class DepartmentController{
    private DepartmentService _departmentService;
    public DepartmentController(DepartmentService departmentService){
        _departmentService = departmentService;
    }
    [HttpGet]
    public async Task<Response<List<Department>>> GetDepartments(){
        return await _departmentService.GetDepartments();
    }
    [HttpPost]
    public async Task<Response<Department>> InsertDepartment(Department department){
        return await _departmentService.InsertDepartment(department);
    }
    [HttpPut]
    public async Task<Response<Department>> UpdateDepartment(Department department){
        return await _departmentService.UpdateDepartment(department);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteDepartment(int id){
        return await _departmentService.DeleteDepartment(id);
    }
    
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return base.ToString();
    }
}