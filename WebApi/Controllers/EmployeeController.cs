using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class EmployeeController{
    private EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService){
        _employeeService = employeeService;
    }
    [HttpGet]
    public async Task<Response<List<Employee>>> GetEmployees(){
        return await _employeeService.GetEmployees();
    }
    [HttpPost]
    public async Task<Response<Employee>> InsertEmployee(Employee employee){
        return await _employeeService.InsertEmployee(employee);
    }
    [HttpPut]
    public async Task<Response<Employee>> UpdateEmployee(Employee employee){
        return await _employeeService.UpdateEmployee(employee);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteEmployee(int id){
        return await _employeeService.DeleteEmployee(id);
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