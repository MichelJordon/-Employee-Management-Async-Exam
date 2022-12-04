using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class LocationService
{
    private readonly DapperContext _context;
    public LocationService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Location>>> GetLocations()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, street_address as StreetAddress, postal_code as PostalCode, city as City, state_province as StateProvince, country_id as CountryId  from Locations";
            var response = await connection.QueryAsync<Location>(sql);
            return new Response<List<Location>>(response.ToList());
        }
    }
    public async Task<Response<Location>> InsertLocation(Location location)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into Locations(street_address, postal_code, city, state_province, country_id) values ('{location.StreetAddress}',{location.PostalCode}, '{location.city}', '{location.StateProvince}', {location.CountryId} ) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                location.Id = response;
                return new Response<Location>(location);
            }
        }
        catch (Exception ex)
        {
            return new Response<Location>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Location>> UpdateLocation(Location location)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update Locations set street_address = '{location.StreetAddress}', postal_code = {location.PostalCode}, city = '{location.city}', state_province = '{location.StateProvince}', country_id = {location.CountryId}  where id = {location.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                location.Id = response;
                return new Response<Location>(location);
            }
        }
        catch (Exception ex)
        {
            return new Response<Location>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteLocation(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from Locations where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}