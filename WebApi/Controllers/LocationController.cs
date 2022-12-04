using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class LocationController{
    private LocationService _locationService;
    public LocationController(LocationService locationService){
        _locationService = locationService;
    }
    [HttpGet]
    public async Task<Response<List<Location>>> GetLocations(){
        return await _locationService.GetLocations();
    }
    [HttpPost]
    public async Task<Response<Location>> InsertLocation(Location location){
        return await _locationService.InsertLocation(location);
    }
    [HttpPut]
    public async Task<Response<Location>> UpdateLocation(Location location){
        return await _locationService.UpdateLocation(location);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteLocation(int id){
        return await _locationService.DeleteLocation(id);
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