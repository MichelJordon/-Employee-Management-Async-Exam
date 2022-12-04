using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class RegionController{
    private RegionService _regionService;
    public RegionController(RegionService regionService){
        _regionService = regionService;
    }
    [HttpGet]
    public async Task<Response<List<Region>>> GetRegions(){
        return await _regionService.GetRegions();
    }
    [HttpPost]
    public async Task<Response<Region>> InsertRegion(Region region){
        return await _regionService.InsertRegion(region);
    }
    [HttpPut]
    public async Task<Response<Region>> UpdateRegion(Region region){
        return await _regionService.UpdateRegion(region);
    }
     [HttpDelete]
    public async Task<Response<string>> DeleteRegion(int id){
        return await _regionService.DeleteRegion(id);
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