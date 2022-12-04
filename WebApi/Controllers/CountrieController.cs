using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CountrieController{
    private CountrieService _countrieService;
    public CountrieController(CountrieService countrieService){
        _countrieService = countrieService;
    }
    [HttpGet]
    public async Task<Response<List<Countrie>>> GetCountries(){
        return await _countrieService.GetCountries();
    }
    [HttpPost]
    public async Task<Response<Countrie>> InsertCountrie(Countrie countrie){
        return await _countrieService.InsertCountrie(countrie);
    }
    [HttpPut]
    public async Task<Response<Countrie>> UpdateCountrie(Countrie countrie){
        return await _countrieService.UpdateCountrie(countrie);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteCountrie(int id){
        return await _countrieService.DeleteCountrie(id);
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