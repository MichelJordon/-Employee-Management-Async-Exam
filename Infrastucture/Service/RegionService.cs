using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class RegionService
{
    private readonly DapperContext _context;
    public RegionService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Region>>> GetRegions()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, region_name as RegionName from Regions";
            var response = await connection.QueryAsync<Region>(sql);
            return new Response<List<Region>>(response.ToList());
        }
    }
    public async Task<Response<Region>> InsertRegion(Region region)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into Regions(region_name) values ('{region.RegionName}') returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                region.Id = response;
                return new Response<Region>(region);
            }
        }
        catch (Exception ex)
        {
            return new Response<Region>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Region>> UpdateRegion(Region region)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update Regions set region_name = '{region.RegionName}' where id = {region.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                region.Id = response;
                return new Response<Region>(region);
            }
        }
        catch (Exception ex)
        {
            return new Response<Region>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteRegion(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from Regions where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}